using CourseManagement.Domain.Entities;

namespace CourseManagement.Application.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course> GetByIdAsync(int id);
}
