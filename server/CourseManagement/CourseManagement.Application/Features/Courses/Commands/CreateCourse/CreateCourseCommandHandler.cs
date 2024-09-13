using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;
using MediatR;

namespace CourseManagement.Application.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
{
    private readonly ICourseRepository _repository;

    public CreateCourseCommandHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            Subject = request.Subject,
            CourseNumber = request.CourseNumber,
            Description = request.Description
        };

        await _repository.AddAsync(course, cancellationToken);
        return course.Id;
    }
}
