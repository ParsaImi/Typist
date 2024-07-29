using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using MongoDB.Bson;
using System.Security.Cryptography.X509Certificates;

namespace SearchService;

public class Dbinit
{
    static readonly HttpClient client = new HttpClient();
    public static async Task InitDb(WebApplication app)
    {
        
        var connString = MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection"));
        Console.WriteLine(connString);
        await DB.InitAsync("SearchDb" , connString);


        await DB.Index<Item>()
            .Key(x => x.UserName , KeyType.Text)
            .Key(x => x.Email , KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Item>();

        if (app.Environment.IsDevelopment())
        {
            // deleting all data in database
             await DB.DropCollectionAsync<Item>();
                    


            // initializing data from TypisApi service
            using var scope = app.Services.CreateScope();
            var httpClient = scope.ServiceProvider.GetRequiredService<TypistSrvHttpClient>();
            var items = await httpClient.GetItemsForSearchDb();
            Console.WriteLine(items.Count + "returned from the auction server");
            if (items.Count > 0) await DB.SaveAsync(items);
            

            // var itemData = await File.ReadAllTextAsync("Data/users.json");
            // var items = JsonSerializer.Deserialize<List<Item>>(itemData);

             // await DB.InsertAsync(items);
        }

        
    }
    
}
