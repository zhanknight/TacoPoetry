using Microsoft.EntityFrameworkCore;
using TacoMusings.API.Contexts;
using TacoMusings.API.Models;
using TacoMusings.API.Services.Interfaces;
using TacoMusings.API.Utilities;

namespace TacoMusings.API.Services;

public class ContentService : IContentService
{
    private readonly ILogger<ContentService> _logger;
    private readonly TacoMusingsContext _context;

    public ContentService(ILogger<ContentService> logger, TacoMusingsContext context)
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
            .Include(c => c.ContentAuthorNavigation)
            .Include(c => c.ContentTypeNavigation)
            .Include(c => c.TagMap)
            .FirstOrDefaultAsync();

        return content.ToViewModel();
    }

    public async Task<ContentView> CreateContent(Content content)
    {
        _context.Content.Add(content);
        await _context.SaveChangesAsync();

        return content.ToViewModel();
    }

    public async Task<ContentView> UpdateContent(int id, Content content)
    {
        _context.Update(content);
        await _context.SaveChangesAsync();

        return content.ToViewModel();
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
}
