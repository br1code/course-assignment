namespace CourseManagement.Application.Features.Courses.Dtos;

public class CourseDto
{
    public int Id { get; set; }
    public required string Subject { get; set; }
    public required string CourseNumber { get; set; }
    public required string Description { get; set; }
}
