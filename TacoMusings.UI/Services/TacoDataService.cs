using System.Text.Json;
using TacoMusings.UI.Models;
using TacoMusings.UI.Services.Interfaces;

namespace TacoMusings.UI.Services;

public class TacoDataService : ITacoDataService
{
    private readonly HttpClient _httpClient;

    public TacoDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Content>> GetAllContent()
    {
        return await JsonSerializer.DeserializeAsync<IEnumerable<Content>>
            (await _httpClient.GetStreamAsync($"/content"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task<IEnumerable<Content>> GetContentByAuthor(int id)
    {
        return await JsonSerializer.DeserializeAsync<IEnumerable<Content>>
            (await _httpClient.GetStreamAsync($"/content/author/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task<IEnumerable<Content>> GetContentByTag(int id)
    {
        return await JsonSerializer.DeserializeAsync<IEnumerable<Content>>
            (await _httpClient.GetStreamAsync($"/content/tags/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {

        return await JsonSerializer.DeserializeAsync<IEnumerable<Author>>
            (await _httpClient.GetStreamAsync($"/authors"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task<IEnumerable<Tag>> GetAllTags()
    {
        return await JsonSerializer.DeserializeAsync<IEnumerable<Tag>>
                   (await _httpClient.GetStreamAsync($"/tags"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
}
