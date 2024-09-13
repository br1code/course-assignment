using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CourseManagement.Domain.Entities;

namespace CourseManagement.Infrastructure.Data;

public static class DatabaseInitializer
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            Console.WriteLine("Initializing database.");

            await context.Database.MigrateAsync();
            await SeedCourses(context);

            Console.WriteLine("Database initialized successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
            throw;
        }
    }

    private static async Task SeedCourses(ApplicationDbContext context)
    {
        if (!context.Courses.Any())
        {
            Console.WriteLine("Seeding the database with courses ...");

            context.Courses.AddRange(
                new Course { Subject = "BIO", CourseNumber = "101", Description = "Introduction to Biology" },
                new Course { Subject = "MAT", CourseNumber = "045", Description = "Business Statistics" }
            );

            await context.SaveChangesAsync();
        }
    }
}
