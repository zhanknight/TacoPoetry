using TacoPoetry.UI.Models;

namespace TacoPoetry.UI.Services.Interfaces
{
    public interface ITacoDataService
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<IEnumerable<Content>> GetAllContent();
        Task<IEnumerable<Tag>> GetAllTags();
        Task<IEnumerable<Content>> GetContentByAuthor(int id);
        Task<IEnumerable<Content>> GetContentByTag(int id);
    }
}