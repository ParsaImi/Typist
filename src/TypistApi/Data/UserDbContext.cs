using MassTransit;
using Microsoft.EntityFrameworkCore;
using TypistApi.Entity;

namespace TypistApi.Data;

public class UserDbContext : DbContext 
{
  public UserDbContext(DbContextOptions options) : base(options)
  {

  }

  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.AddInboxStateEntity();
    modelBuilder.AddOutboxMessageEntity();
    modelBuilder.AddOutboxStateEntity();
  }
}
