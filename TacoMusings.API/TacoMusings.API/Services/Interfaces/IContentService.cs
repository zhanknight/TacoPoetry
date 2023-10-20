using TacoMusings.API.Models;

namespace TacoMusings.API.Services.Interfaces
{
    public interface IContentService
    {
        Task<ContentView> CreateContent(Content content);
        Task<ContentView> DeleteContent(int id);
        Task<IEnumerable<ContentView>> GetContent();
        Task<ContentView> GetContentById(int id);
        Task<ContentView> UpdateContent(int id, Content content);
        Task<bool> ContentExists(int id);
    }
}