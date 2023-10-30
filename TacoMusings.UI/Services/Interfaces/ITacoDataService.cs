using TacoMusings.UI.Models;

namespace TacoMusings.UI.Services.Interfaces
{
    public interface ITacoDataService
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<IEnumerable<Content>> GetAllContent();
        Task<IEnumerable<String>> GetAllTags();
    }
}