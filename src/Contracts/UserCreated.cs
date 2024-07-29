namespace Contracts;

public class UserCreated
{
    public Guid Id {get; set;}

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public int Wins {get; set;}

    public int Loses {get ; set;}

    public int Level {get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string ImageUrl {get; set;}
}
