using Microsoft.EntityFrameworkCore;
using TacoMusings.API.Contexts;
using TacoMusings.API.Models;
using TacoMusings.API.Services.Interfaces;

namespace TacoMusings.API.Services;

public class TagService : ITagService
{
    private readonly ILogger<TagService> _logger;
    private readonly TacoMusingsContext _context;

    public TagService(ILogger<TagService> logger, TacoMusingsContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IEnumerable<Tag>> GetAllTags()
    {
        var tags = await _context.Tag.ToListAsync();
        return tags;
    }

    public async Task<Tag> GetTagById(int id)
    {
        var tag = await _context.Tag.FindAsync(id);
        return await _context.Tag.FindAsync(id);
    }

    public async Task<IEnumerable<Tag>> GetTagByContentId(int contentId)
    {
        var mappedTags = await _context.TagMap.Where(t => t.MappedContentId == contentId).ToListAsync();
        var test = await _context.Tag.Where(t => _context.TagMap.Where(t => t.MappedContentId == contentId).Any(mt => mt.MappedTagId == t.TagId)).ToListAsync();
        return test;
    }

    public async Task<Tag> CreateTag(string tag)
    {
        throw new NotImplementedException();
    }

    public async Task<Tag> UpdateTag(int id, Tag tag)
    {
        _context.Entry(tag).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return tag;
    }

    public async Task DeleteTag(int id)
    {
        var tag = await _context.Tag.FindAsync(id);
        _context.Tag.Remove(tag);
        await _context.SaveChangesAsync();
    }
}
