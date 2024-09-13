using CourseManagement.Domain.Entities;

namespace CourseManagement.Application.Interfaces;

public interface ICourseRepository
{
    Task<List<Course>> GetCoursesAsync(string? description = null, CancellationToken cancellationToken = default);
    Task<Course?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
