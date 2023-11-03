using TacoPoetry.API.Models;

namespace TacoPoetry.API.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorView> CreateAuthor(AuthorCreate author);
        Task<AuthorView> DeleteAuthor(int id);
        Task<AuthorView> GetAuthorById(int id);
        Task<IEnumerable<AuthorView>> GetAuthors();
        Task<AuthorView> UpdateAuthor(int id, AuthorCreate author);
        Task<bool> AuthorExists(int id);

    }
}