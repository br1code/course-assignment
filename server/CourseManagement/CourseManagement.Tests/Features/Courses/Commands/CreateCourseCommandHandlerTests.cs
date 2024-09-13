using Moq;
using CourseManagement.Application.Features.Courses.Commands.CreateCourse;
using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;

namespace CourseManagement.Tests.Features.Courses.Commands;

public class CreateCourseCommandHandlerTests
{
    private readonly Mock<ICourseRepository> _repositoryMock;
    private readonly CreateCourseCommandHandler _handler;

    public CreateCourseCommandHandlerTests()
    {
        _repositoryMock = new Mock<ICourseRepository>();
        _handler = new CreateCourseCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_AddsCourse()
    {
        // Arrange
        var command = new CreateCourseCommand
        {
            Subject = "PHY",
            CourseNumber = "201",
            Description = "Physics II"
        };

        var course = new Course
        {
            Id = 1,
            Subject = command.Subject,
            CourseNumber = command.CourseNumber,
            Description = command.Description
        };

        _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>()))
            .Callback<Course, CancellationToken>((c, ct) => c.Id = course.Id)
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(course.Id, result);
        _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
