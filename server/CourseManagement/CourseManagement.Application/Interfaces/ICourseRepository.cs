using CourseManagement.Domain.Entities;

namespace CourseManagement.Application.Interfaces;

public interface ICourseRepository
{
    Task<List<Course>> GetCoursesAsync(string? description = null, CancellationToken cancellationToken = default);
    Task<Course?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Course course, CancellationToken cancellationToken = default);
    Task<bool> CourseExistsAsync(string subject, string courseNumber, CancellationToken cancellationToken = default);
}
