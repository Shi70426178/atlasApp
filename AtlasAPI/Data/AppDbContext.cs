using Microsoft.EntityFrameworkCore;
using AtlasAPI.Models;

namespace AtlasAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Existing tables
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users => Set<User>();


    // New tables
    public DbSet<Meeting> Meetings => Set<Meeting>();
    public DbSet<MeetingGrid> MeetingGrids => Set<MeetingGrid>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Meeting>()
            .HasMany(m => m.MeetingGrids)
            .WithOne(g => g.Meeting)
            .HasForeignKey(g => g.TMG_TM_Id);
    }
}