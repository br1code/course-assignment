using MediatR;
using Microsoft.AspNetCore.Mvc;
using CourseManagement.Application.Features.Courses.Dtos;
using CourseManagement.Application.Features.Courses.Queries.GetCourses;

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
    }
}
