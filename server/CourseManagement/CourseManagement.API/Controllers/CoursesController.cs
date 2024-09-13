using MediatR;
using Microsoft.AspNetCore.Mvc;
using CourseManagement.Application.Features.Courses.Dtos;
using CourseManagement.Application.Features.Courses.Queries.GetCourses;
using CourseManagement.Application.Features.Courses.Commands.CreateCourse;
using CourseManagement.Application.Features.Courses.Queries.GetCourseById;

namespace CourseManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a list of courses. Optionally filters by description.
        /// </summary>
        /// <param name="description">Optional description to filter courses by.</param>
        /// <returns>A list of courses.</returns>
        /// <response code="200">Returns the list of courses</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), 200)]
        public async Task<IActionResult> GetCourses([FromQuery] string? description = null)
        {
            var query = new GetCoursesQuery { Description = description };
            var courses = await _mediator.Send(query);
            return Ok(courses);
        }

        /// <summary>
        /// Retrieves a course by ID.
        /// </summary>
        /// <param name="id">The ID of the course.</param>
        /// <returns>The course.</returns>
        /// <response code="200">Returns the course.</response>
        /// <response code="404">Course not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CourseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var query = new GetCourseByIdQuery { Id = id };
            var course = await _mediator.Send(query);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        /// <summary>
        /// Inserts a new course.
        /// </summary>
        /// <param name="command">The course to create.</param>
        /// <returns>The ID of the created course.</returns>
        /// <response code="201">Course created successfully.</response>
        /// <response code="400">Validation error occurred.</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
        {
            var courseId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCourseById), new { id = courseId }, null);
        }
    }
}
