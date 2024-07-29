namespace TypistApi.DTOs;

public class UserDto
{
     public string UserName { get; set; }

     public int Wins {get; set;}

     public int Loses {get ; set;}

     public int Level {get; set;}

     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

     public string ImageUrl {get; set;}
}
