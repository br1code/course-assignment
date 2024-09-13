using Moq;
using CourseManagement.Application.Features.Courses.Queries.GetCourses;
using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;

namespace CourseManagement.Tests.Features.Courses.Queries;

public class GetCoursesQueryHandlerTests
{
    private readonly Mock<ICourseRepository> _repositoryMock;
    private readonly GetCoursesQueryHandler _handler;

    public GetCoursesQueryHandlerTests()
    {
        _repositoryMock = new Mock<ICourseRepository>();
        _handler = new GetCoursesQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_NoDescription_ReturnsAllCourses()
    {
        // Arrange
        var courses = new List<Course>
        {
            new Course { Id = 1, Subject = "BIO", CourseNumber = "101", Description = "Introduction to Biology" },
            new Course { Id = 2, Subject = "MAT", CourseNumber = "045", Description = "Business Statistics" }
        };

        _repositoryMock
            .Setup(repo => repo.GetCoursesAsync(null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(courses);

        var query = new GetCoursesQuery { Description = null };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _repositoryMock.Verify(repo => repo.GetCoursesAsync(null, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithDescription_ReturnsFilteredCourses()
    {
        // Arrange
        var courses = new List<Course>
        {
            new Course { Id = 1, Subject = "BIO", CourseNumber = "101", Description = "Introduction to Biology" }
        };

        _repositoryMock
            .Setup(repo => repo.GetCoursesAsync("Biology", It.IsAny<CancellationToken>()))
            .ReturnsAsync(courses);

        var query = new GetCoursesQuery { Description = "Biology" };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Introduction to Biology", result.First().Description);
        _repositoryMock.Verify(repo => repo.GetCoursesAsync("Biology", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithDescription_NoMatches_ReturnsEmptyList()
    {
        // Arrange
        _repositoryMock
            .Setup(repo => repo.GetCoursesAsync("Physics", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Course>());

        var query = new GetCoursesQuery { Description = "Physics" };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _repositoryMock.Verify(repo => repo.GetCoursesAsync("Physics", It.IsAny<CancellationToken>()), Times.Once);
    }
}
