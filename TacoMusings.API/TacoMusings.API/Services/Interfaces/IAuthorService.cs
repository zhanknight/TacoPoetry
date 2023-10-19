using TacoMusings.API.Models;

namespace TacoMusings.API.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<Author> CreateAuthor(Author author);
        Task<Author> DeleteAuthor(int id);
        Task<Author> GetAuthorById(int id);
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> UpdateAuthor(int id, Author author);
    }
}