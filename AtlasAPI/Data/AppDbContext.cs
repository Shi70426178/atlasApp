using Microsoft.EntityFrameworkCore;
using AtlasAPI.Models;
using AtlasAPI.Data;

namespace AtlasAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users => Set<User>();

}