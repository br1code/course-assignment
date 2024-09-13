using Microsoft.EntityFrameworkCore;
using CourseManagement.Domain.Entities;

namespace CourseManagement.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasIndex(e => new { e.Subject, e.CourseNumber }).IsUnique();

            entity.Property(e => e.CourseNumber)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength();

            entity.Property(e => e.Subject)
                    .IsRequired();

            entity.Property(e => e.Description)
                   .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
