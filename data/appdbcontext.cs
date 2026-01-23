using Microsoft.EntityFrameworkCore;
using UniConnect.Api.Models;

namespace UniConnect.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<GuidanceRequest> GuidanceRequests => Set<GuidanceRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<GuidanceRequest>()
            .HasOne(r => r.Student)
            .WithMany()
            .HasForeignKey(r => r.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GuidanceRequest>()
            .HasOne(r => r.Mentor)
            .WithMany()
            .HasForeignKey(r => r.MentorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
