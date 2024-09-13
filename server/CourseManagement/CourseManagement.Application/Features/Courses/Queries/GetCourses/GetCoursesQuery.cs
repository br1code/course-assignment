using MediatR;
using CourseManagement.Application.Features.Courses.Dtos;

namespace CourseManagement.Application.Features.Courses.Queries.GetCourses;

public class GetCoursesQuery : IRequest<IEnumerable<CourseDto>>
{
    public string? Description { get; set; }
}
