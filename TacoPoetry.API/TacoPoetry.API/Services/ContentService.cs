using Microsoft.EntityFrameworkCore;
using TacoPoetry.API.Contexts;
using TacoPoetry.API.Models;
using TacoPoetry.API.Services.Interfaces;
using TacoPoetry.API.Utilities;

namespace TacoPoetry.API.Services;

public class ContentService : IContentService
{
    private readonly ILogger<ContentService> _logger;
    private readonly TacoPoetryContext _context;

    public ContentService(ILogger<ContentService> logger, TacoPoetryContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IEnumerable<ContentView>> GetContent()
    {
        var content = await _context.Content
            .Include(c => c.ContentAuthorNavigation)
            .Include(c => c.ContentTypeNavigation)
            .Include(c => c.TagMap)
            .ToListAsync();

        List<ContentView> contentViews = new List<ContentView>();

        foreach (var c in content)
        {
            contentViews.Add(c.ToViewModel());
        }

        return contentViews;
    }

    public async Task<ContentView> GetContentById(int id)
    {
        var content = await _context.Content
            .Where(c => c.ContentId == id)
            .Include(c => c.ContentAuthorNavigation)
            .Include(c => c.ContentTypeNavigation)
            .Include(c => c.TagMap)
            .FirstOrDefaultAsync();

        return content.ToViewModel();
    }

    public async Task<IEnumerable<ContentView>> GetContentByAuthor(int authorId)
    {
        var content = await _context.Content
            .Where(c => c.ContentAuthor == authorId)
            .Include(c => c.ContentAuthorNavigation)
            .Include(c => c.ContentTypeNavigation)
            .Include(c => c.TagMap)
            .ToListAsync();

        List<ContentView> contentViews = new List<ContentView>();

        foreach (var c in content)
        {
            contentViews.Add(c.ToViewModel());
        }

        return contentViews;
    }

    public async Task<IEnumerable<ContentView>> GetContentByTag(int tagId)
    {
        var content = await _context.Content
            .Where(c => _context.TagMap.Where(t => t.MappedContentId == c.ContentId).Any(mt => mt.MappedTagId == tagId))
            .Include(c => c.ContentAuthorNavigation)
            .Include(c => c.ContentTypeNavigation)
            .Include(c => c.TagMap)
            .ToListAsync();

        List<ContentView> contentViews = new List<ContentView>();

        foreach (var c in content)
        {
            contentViews.Add(c.ToViewModel());
        }

        return contentViews;
    }

    public async Task<ContentView> CreateContent(ContentCreate content)
    {
        var newContent = content.ToEntityModel();

        _context.Content.Add(newContent);
        await _context.SaveChangesAsync();

        var allTags = await _context.Tag.ToListAsync();

        foreach (var tag in content.Tags)
        {
            var normalizedTag = tag.Trim().ToLower();

            var tagExists = allTags.Any(t => t.TagName == normalizedTag);

            if (tagExists)
            {
                var existingTag = allTags.FirstOrDefault(t => t.TagName == normalizedTag).TagId;
                var postedTagMap = await PostTagMap(existingTag, newContent.ContentId);
            }
            else
            {
                var postedTag = await PostTag(normalizedTag);
                var postedTagMap = await PostTagMap(postedTag.TagId, newContent.ContentId);
            }
        }

        _context.Entry(newContent).Reference(c => c.ContentAuthorNavigation).Load();

        return newContent.ToViewModel();
    }

    public async Task<ContentView> UpdateContent(int id, ContentCreate content)
    {
        var existingContent = await _context.Content
            .Where(c => c.ContentId == id)
            .Include(c => c.ContentAuthorNavigation)
            .Include(c => c.ContentTypeNavigation)
            .Include(c => c.TagMap)
            .FirstOrDefaultAsync();

        var incomingContent = content.ToEntityModel();

        existingContent.ContentTitle = incomingContent.ContentTitle;
        existingContent.ContentBody = incomingContent.ContentBody;
        existingContent.ContentDate = incomingContent.ContentDate;
        existingContent.ContentSource = incomingContent.ContentSource;
        existingContent.ContentAuthor = incomingContent.ContentAuthor;
        existingContent.ContentType = incomingContent.ContentType;


        // tag updates are messy right now 

        var allTags = await _context.Tag.ToListAsync();
        var existingTags = existingContent.TagMap.Select(tm => tm.MappedTag.TagName).ToList();

        var removedTags = existingTags.Where(p => !content.Tags.Any(p2 => p2 == p));
        var removedTags2 = content.Tags.Where(p => !existingTags.Any(p2 => p2 == p));

        if (!removedTags.Any() && !removedTags2.Any())
        {
            // no tag changes detected
        }
        else
        {
            // delete all existing tags
            foreach (var tm in existingContent.TagMap)
            {
                _context.TagMap.Remove(tm);
                _logger.LogInformation("Removing tagmap", tm);
            }

            foreach (var tag in content.Tags)
            {
                // normalize the tag
                var normalizedTag = tag.Trim().ToLower();
                // check if the tag exists in the current content
                var currentlyTagged = existingTags.Any(t => t == normalizedTag);
                // if it does, add a new tagmap anyway because we just wiped all existing ones
                if (currentlyTagged)
                {
                    var existingTag = allTags.FirstOrDefault(t => t.TagName == normalizedTag).TagId;
                    var postedTagMap = await PostTagMap(existingTag, id);
                }
                // if it doesn't, check if tag exists in the database
                if (!currentlyTagged)
                {
                    var tagExists = allTags.Any(t => t.TagName == normalizedTag);

                    // if it does, add the tag to the content with a tagmap
                    if (tagExists)
                    {
                        var existingTag = allTags.FirstOrDefault(t => t.TagName == normalizedTag).TagId;
                        var postedTagMap = await PostTagMap(existingTag, id);
                    }
                    // if it doesn't, add it to the db on tags and then tagmap it
                    else
                    {
                        var postedTag = await PostTag(normalizedTag);
                        var postedTagMap = await PostTagMap(postedTag.TagId, id);
                    }
                }
            }
        }

        _context.Update(existingContent);
        await _context.SaveChangesAsync();

        _context.Entry(existingContent).Reference(c => c.ContentAuthorNavigation).Load();
        _context.Entry(existingContent).Collection(c => c.TagMap).Load();

        return existingContent.ToViewModel();
    }

    public async Task<ContentView> DeleteContent(int id)
    {
        var content = await _context.Content.FindAsync(id);
        _context.Content.Remove(content);
        await _context.SaveChangesAsync();
        return content.ToViewModel();
    }

    public async Task<bool> ContentExists(int id)
    {
        var contentExists = await _context.Content.AnyAsync(c => c.ContentId == id);
        return contentExists;
    }

    public async Task<Tag> PostTag(string tag)
    {
        var newTag = new Tag
        {
            TagName = tag.Trim().ToLower()
        };

        _context.Tag.Add(newTag);

        await _context.SaveChangesAsync();

        return newTag;
    }

    public async Task<TagMap> PostTagMap(int tagId, int contentId)
    {
        var newTagMap = new TagMap
        {
            MappedTagId = tagId,
            MappedContentId = contentId
        };

        _context.TagMap.Add(newTagMap);

        await _context.SaveChangesAsync();

        return newTagMap;
    }
}
