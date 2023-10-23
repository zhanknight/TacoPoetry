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
            (await _httpClient.GetStreamAsync($"/api/content"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await JsonSerializer.DeserializeAsync<IEnumerable<Author>>
            (await _httpClient.GetStreamAsync($"/api/content"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

}
