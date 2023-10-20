using Microsoft.EntityFrameworkCore;
using TacoMusings.API.Contexts;
using TacoMusings.API.Models;
using TacoMusings.API.Services.Interfaces;
using TacoMusings.API.Utilities;

namespace TacoMusings.API.Services;

public class AuthorService : IAuthorService
{
    private readonly ILogger<AuthorService> _logger;
    private readonly TacoMusingsContext _context;

    public AuthorService(ILogger<AuthorService> logger, TacoMusingsContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IEnumerable<AuthorView>> GetAuthors()
    {
        var authors = await _context.Author.ToListAsync();
        List<AuthorView> authorViews = new List<AuthorView>();

        foreach (var a in authors)
        {
            authorViews.Add(a.ToViewModel());
        }

        return authorViews;
    }

    public async Task<AuthorView> GetAuthorById(int id)
    {
        var author = await _context.Author.Where(a => a.AuthorId == id).FirstOrDefaultAsync();

        return author.ToViewModel();        
    }

    public async Task<AuthorView> CreateAuthor(Author author)
    {
        _context.Author.Add(author);
        await _context.SaveChangesAsync();

        return author.ToViewModel();
    }

    public async Task<AuthorView> UpdateAuthor(int id, Author author)
    {
        _context.Update(author);
        await _context.SaveChangesAsync();

        return author.ToViewModel();
    }

    public async Task<AuthorView> DeleteAuthor(int id)
    {
        var author = await _context.Author.FindAsync(id);
        _context.Author.Remove(author);
        await _context.SaveChangesAsync();
        return author.ToViewModel();
    }

    public async Task<bool> AuthorExists(int id)
    {
        var authorExists = await _context.Author.AnyAsync(a => a.AuthorId == id);
        return authorExists;
    }
}
