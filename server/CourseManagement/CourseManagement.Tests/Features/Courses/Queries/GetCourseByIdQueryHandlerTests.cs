using Moq;
using CourseManagement.Application.Features.Courses.Queries.GetCourseById;
using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;

namespace CourseManagement.Tests.Features.Courses.Queries;

public class GetCourseByIdQueryHandlerTests
{
    private readonly Mock<ICourseRepository> _repositoryMock;
    private readonly GetCourseByIdQueryHandler _handler;

    public GetCourseByIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<ICourseRepository>();
        _handler = new GetCourseByIdQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_CourseExists_ReturnsCourseDto()
    {
        // Arrange
        var course = new Course
        {
            Id = 1,
            Subject = "BIO",
            CourseNumber = "101",
            Description = "Introduction to Biology"
        };

        _repositoryMock.Setup(repo => repo.GetByIdAsync(course.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(course);

        var query = new GetCourseByIdQuery { Id = course.Id };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(course.Id, result.Id);
        Assert.Equal(course.Subject, result.Subject);
        Assert.Equal(course.CourseNumber, result.CourseNumber);
        Assert.Equal(course.Description, result.Description);
        _repositoryMock.Verify(repo => repo.GetByIdAsync(course.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_CourseDoesNotExist_ReturnsNull()
    {
        // Arrange
        _repositoryMock.Setup(repo => repo.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(value: null);

        var query = new GetCourseByIdQuery { Id = 1 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
        _repositoryMock.Verify(repo => repo.GetByIdAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }
}
