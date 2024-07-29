using Microsoft.EntityFrameworkCore;
using TypistApi.Entity;

namespace TypistApi.Data;

public class Dbinitializer
{
    public static void InitDb(WebApplication app)
    {
        Console.WriteLine("test mikonammmmmmmmmmmmmmmmmmmmm");
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<UserDbContext>());
    }

    private static void SeedData(UserDbContext context)
    {
        context.Database.Migrate();

        if(context.Users.Any())
        {
            Console.WriteLine("Already have data , no need to seed!");
            return;
        }

        var users = new List<User>()
        {
               // 1 Ford GT
            new User
            {
                Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                UserName = "parsa",
                Password = "123",
                Email = "parsaemani17@gmail.com",
                Wins = 51,
                Loses = 6,
                Level = 20,
                CreatedAt = DateTime.UtcNow,
                ImageUrl = "example-image",
            // 2 Bugatti Veyron
            
        },

            new User
            {
                Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1ef80b833a9f"),
                UserName = "jadi",
                Password = "123",
                Email = "jadijadi@gmail.com",
                Wins = 16,
                Loses = 3,
                Level = 16,
                CreatedAt = DateTime.UtcNow,
                ImageUrl = "example-image",
            // 2 Bugatti Veyron
            
        },
            new User
            {
                Id = Guid.Parse("bbab4d5a-8565-48b1-9450-5ac2a5c4a654"),
                UserName = "fill",
                Password = "123",
                Email = "filfoden@gmail.com",
                Wins = 2,
                Loses = 7,
                Level = 1,
                CreatedAt = DateTime.UtcNow,
                ImageUrl = "example-image",
            // 2 Bugatti Veyron
            
        },

    };
        context.AddRange(users);
        context.SaveChanges(); 
    }
}
