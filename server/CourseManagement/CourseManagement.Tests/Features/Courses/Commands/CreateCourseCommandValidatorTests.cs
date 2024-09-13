using Moq;
using CourseManagement.Application.Features.Courses.Commands.CreateCourse;
using CourseManagement.Application.Interfaces;

namespace CourseManagement.Tests.Features.Courses.Commands;

public class CreateCourseCommandValidatorTests
{
    private readonly CreateCourseCommandValidator _validator;
    private readonly Mock<ICourseRepository> _repositoryMock;

    public CreateCourseCommandValidatorTests()
    {
        _repositoryMock = new Mock<ICourseRepository>();
        _validator = new CreateCourseCommandValidator(_repositoryMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ReturnsSuccess()
    {
        // Arrange
        var command = new CreateCourseCommand
        {
            Subject = "PHY",
            CourseNumber = "201",
            Description = "Physics II"
        };

        _repositoryMock.Setup(repo => repo.CourseExistsAsync(command.Subject, command.CourseNumber, default))
            .ReturnsAsync(false);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_DuplicateCourse_ReturnsFailure()
    {
        // Arrange
        var command = new CreateCourseCommand
        {
            Subject = "PHY",
            CourseNumber = "201",
            Description = "Physics II"
        };

        _repositoryMock.Setup(repo => repo.CourseExistsAsync(command.Subject, command.CourseNumber, default))
            .ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == CreateCourseCommandValidator.DUPLICATED_COURSE_ERROR_MESSAGE);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Validate_InvalidSubject_ReturnsFailure(string subject)
    {
        // Arrange
        var command = new CreateCourseCommand
        {
            Subject = subject,
            CourseNumber = "201",
            Description = "Physics II"
        };

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Subject");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("20")]
    [InlineData("2010")]
    [InlineData("abc")]
    public async Task Validate_InvalidCourseNumber_ReturnsFailure(string courseNumber)
    {
        // Arrange
        var command = new CreateCourseCommand
        {
            Subject = "PHY",
            CourseNumber = courseNumber,
            Description = "Physics II"
        };

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "CourseNumber");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Validate_InvalidDescription_ReturnsFailure(string description)
    {
        // Arrange
        var command = new CreateCourseCommand
        {
            Subject = "PHY",
            CourseNumber = "201",
            Description = description
        };

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description");
    }
}
