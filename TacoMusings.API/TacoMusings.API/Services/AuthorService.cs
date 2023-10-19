using Microsoft.EntityFrameworkCore;
using TacoMusings.API.Contexts;
using TacoMusings.API.Models;
using TacoMusings.API.Services.Interfaces;

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

    public async Task<IEnumerable<Author>> GetAuthors()
    {
        var authors = await _context.Author.ToListAsync();
        return authors;
    }

    public async Task<Author> GetAuthorById(int id)
    {
        var author = await _context.Author.FindAsync(id);
        return await _context.Author.FindAsync(id);
    }

    public async Task<Author> CreateAuthor(Author author)
    {
        _context.Author.Add(author);
        await _context.SaveChangesAsync();

        return author;
    }

    public async Task<Author> UpdateAuthor(int id, Author author)
    {
        _context.Update(author);
        await _context.SaveChangesAsync();

        return author;
    }

    public async Task<Author> DeleteAuthor(int id)
    {
        var author = await _context.Author.FindAsync(id);
        _context.Author.Remove(author);
        await _context.SaveChangesAsync();
        return author;
    }
}
