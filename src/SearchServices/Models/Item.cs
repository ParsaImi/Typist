using MongoDB.Entities;

namespace SearchService;

public class Item : Entity
{
    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public int Wins {get; set;}

    public int Loses {get ; set;}

    public int Level {get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string ImageUrl {get; set;}

}
