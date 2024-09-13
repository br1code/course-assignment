using CourseManagement.Application.Features.Courses.Dtos;
using MediatR;

namespace CourseManagement.Application.Features.Courses.Queries.GetCourseById;

public class GetCourseByIdQuery : IRequest<CourseDto>
{
    public int Id { get; set; }
}
