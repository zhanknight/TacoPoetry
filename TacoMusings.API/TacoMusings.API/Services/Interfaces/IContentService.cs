using TacoMusings.API.Models;

namespace TacoMusings.API.Services.Interfaces
{
    public interface IContentService
    {
        Task<ContentView> CreateContent(ContentCreate content);
        Task<ContentView> DeleteContent(int id);
        Task<IEnumerable<ContentView>> GetContent();
        Task<ContentView> GetContentById(int id);
        Task<IEnumerable<ContentView>> GetContentByAuthor(int authorId);
        Task<ContentView> UpdateContent(int id, ContentCreate content);
        Task<bool> ContentExists(int id);
    }
}