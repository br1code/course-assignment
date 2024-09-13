using Moq;
using MediatR;
using CourseManagement.Application.Exceptions;
using CourseManagement.Application.Features.Courses.Commands.DeleteCourse;
using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;

namespace CourseManagement.Tests.Features.Courses.Commands;

public class DeleteCourseCommandHandlerTests
{
    private readonly Mock<ICourseRepository> _repositoryMock;
    private readonly DeleteCourseCommandHandler _handler;

    public DeleteCourseCommandHandlerTests()
    {
        _repositoryMock = new Mock<ICourseRepository>();
        _handler = new DeleteCourseCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_CourseExists_DeletesCourse()
    {
        // Arrange
        var courseId = 1;
        var course = new Course 
        { 
            Id = courseId, 
            Subject = "BIO", 
            CourseNumber = "101", 
            Description = "Introduction to Biology" 
        };

        _repositoryMock.Setup(repo => repo.GetByIdAsync(courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(course);

        _repositoryMock.Setup(repo => repo.DeleteAsync(course, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var command = new DeleteCourseCommand { Id = courseId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(Unit.Value, result);
        _repositoryMock.Verify(repo => repo.GetByIdAsync(courseId, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(repo => repo.DeleteAsync(course, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_CourseDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var courseId = 1;

        _repositoryMock.Setup(repo => repo.GetByIdAsync(courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(value: null);

        var command = new DeleteCourseCommand { Id = courseId };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));

        _repositoryMock.Verify(repo => repo.GetByIdAsync(courseId, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
