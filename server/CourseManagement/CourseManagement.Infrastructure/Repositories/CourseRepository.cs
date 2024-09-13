using Microsoft.EntityFrameworkCore;
using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;
using CourseManagement.Infrastructure.Data;

namespace CourseManagement.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetCoursesAsync(string? description = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Courses.AsNoTracking();

        if (!string.IsNullOrEmpty(description))
        {
            query = query.Where(c => c.Description.Contains(description));
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Course?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task AddAsync(Course course, CancellationToken cancellationToken = default)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> CourseExistsAsync(string subject, string courseNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            .AnyAsync(c => c.Subject == subject && c.CourseNumber == courseNumber, cancellationToken);
    }
}
