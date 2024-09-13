using MediatR;
using CourseManagement.Domain.Entities;
using CourseManagement.Application.Exceptions;
using CourseManagement.Application.Interfaces;

namespace CourseManagement.Application.Features.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Unit>
{
    private readonly ICourseRepository _repository;

    public DeleteCourseCommandHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (course == null)
        {
            throw new NotFoundException(nameof(Course), request.Id);
        }

        await _repository.DeleteAsync(course, cancellationToken);
        return Unit.Value;
    }
}
