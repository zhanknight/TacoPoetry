using Microsoft.EntityFrameworkCore;
using TacoMusings.API.Contexts;
using TacoMusings.API.Models;
using TacoMusings.API.Services.Interfaces;

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

    public async Task<IEnumerable<Content>> GetContent()
    {
        var content = await _context.Content.ToListAsync();
        return content;
    }

    public async Task<Content> GetContentById(int id)
    {
        var content = await _context.Content.FindAsync(id);
        return await _context.Content.FindAsync(id);
    }

    public async Task<Content> CreateContent(Content content)
    {
        _context.Content.Add(content);
        await _context.SaveChangesAsync();

        return content;
    }

    public async Task<Content> UpdateContent(int id, Content content)
    {
        _context.Entry(content).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return content;
    }

    public async Task DeleteContent(int id)
    {
        var content = await _context.Content.FindAsync(id);
        _context.Content.Remove(content);
        await _context.SaveChangesAsync();
    }
}
