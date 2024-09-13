using MediatR;
using CourseManagement.Application.Features.Courses.Dtos;
using CourseManagement.Application.Interfaces;

namespace CourseManagement.Application.Features.Courses.Queries.GetCourses;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IEnumerable<CourseDto>>
{
    private readonly ICourseRepository _repository;

    public GetCoursesQueryHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _repository.GetCoursesAsync(request.Description, cancellationToken);

        return courses.Select(course => new CourseDto
        {
            Id = course.Id,
            Subject = course.Subject,
            CourseNumber = course.CourseNumber,
            Description = course.Description
        });
    }
}
