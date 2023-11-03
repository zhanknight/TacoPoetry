using Microsoft.EntityFrameworkCore;
using TacoPoetry.API.Contexts;
using TacoPoetry.API.Models;
using TacoPoetry.API.Services.Interfaces;
using TacoPoetry.API.Utilities;

namespace TacoPoetry.API.Services;

public class AuthorService : IAuthorService
{
    private readonly ILogger<AuthorService> _logger;
    private readonly TacoPoetryContext _context;

    public AuthorService(ILogger<AuthorService> logger, TacoPoetryContext context)
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

    public async Task<AuthorView> CreateAuthor(AuthorCreate author)
    {
        var newAuthor = author.ToEntityModel();
        _context.Author.Add(newAuthor);
        await _context.SaveChangesAsync();

        return newAuthor.ToViewModel();
    }

    public async Task<AuthorView> UpdateAuthor(int id, AuthorCreate author)
    {
        var existingAuthor = await _context.Author.FindAsync(id);
        var incomingAuthor = author.ToEntityModel();

        existingAuthor.AuthorName = incomingAuthor.AuthorName;
        existingAuthor.AuthorBio = incomingAuthor.AuthorBio;
        existingAuthor.AuthorPhotoId = incomingAuthor.AuthorPhotoId;

        _context.Update(existingAuthor);
        await _context.SaveChangesAsync();

        return existingAuthor.ToViewModel();
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
