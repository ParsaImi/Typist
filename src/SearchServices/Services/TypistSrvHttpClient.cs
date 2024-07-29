using MongoDB.Entities;

namespace SearchService;

public class TypistSrvHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public TypistSrvHttpClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<List<Item>> GetItemsForSearchDb()
    {
        var lastUpdated = await DB.Find<Item , string>()
        .Sort(x => x.Descending(x => x.CreatedAt))
        .Project(x => x.CreatedAt.ToString())
        .ExecuteFirstAsync();
        return await _httpClient.GetFromJsonAsync<List<Item>>(_config["TypistApiServiceUrl"] + "/api/users" + lastUpdated);

    }
}
