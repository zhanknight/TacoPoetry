using TacoPoetry.API.Models;

namespace TacoPoetry.API.Services.Interfaces
{
    public interface ITagService
    {
        Task<Tag> CreateTag(string tag);
        Task DeleteTag(int id);
        Task<IEnumerable<Tag>> GetAllTags();
        Task<IEnumerable<Tag>> GetTagByContentId(int contentId);
        Task<Tag> GetTagById(int id);
        Task<Tag> UpdateTag(int id, Tag tag);
        Task<bool> TagExists(int id);
    }
}