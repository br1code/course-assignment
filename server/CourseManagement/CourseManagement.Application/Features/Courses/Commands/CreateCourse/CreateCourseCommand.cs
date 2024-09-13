using MediatR;

namespace CourseManagement.Application.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommand : IRequest<int>
{
    public required string Subject { get; set; }
    public required string CourseNumber { get; set; }
    public required string Description { get; set; }
}
