using TacoMusings.API.Models;

namespace TacoMusings.API.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorView> CreateAuthor(Author author);
        Task<AuthorView> DeleteAuthor(int id);
        Task<AuthorView> GetAuthorById(int id);
        Task<IEnumerable<AuthorView>> GetAuthors();
        Task<AuthorView> UpdateAuthor(int id, Author author);
        Task<bool> AuthorExists(int id);

    }
}