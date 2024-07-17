using Microsoft.EntityFrameworkCore;
using TypistApi.Entity;

namespace TypistApi.Data;

public class UserDbContext : DbContext 
{
  public UserDbContext(DbContextOptions options) : base(options)
  {

  }

  public DbSet<User> Users { get; set; }
}
