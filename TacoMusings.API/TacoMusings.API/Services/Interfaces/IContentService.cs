using TacoMusings.API.Models;

namespace TacoMusings.API.Services.Interfaces
{
    public interface IContentService
    {
        Task<Content> CreateContent(Content content);
        Task DeleteContent(int id);
        Task<IEnumerable<Content>> GetContent();
        Task<Content> GetContentById(int id);
        Task<Content> UpdateContent(int id, Content content);
    }
}