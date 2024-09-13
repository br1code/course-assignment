using CourseManagement.Application.Features.Courses.Dtos;
using CourseManagement.Application.Interfaces;
using MediatR;

namespace CourseManagement.Application.Features.Courses.Queries.GetCourseById;

public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDto?>
{
    private readonly ICourseRepository _repository;

    public GetCourseByIdQueryHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<CourseDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (course == null)
        {
            return null;
        }

        return new CourseDto
        {
            Id = course.Id,
            Subject = course.Subject,
            CourseNumber = course.CourseNumber,
            Description = course.Description
        };
    }
}
