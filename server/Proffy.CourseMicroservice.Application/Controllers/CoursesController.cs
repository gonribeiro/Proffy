using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proffy.CourseMicroservice.Domain.AggregatesModel.CourseAggregate;
using Proffy.CourseMicroservice.Infrastructure.DataAccess.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proffy.CourseMicroservice.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseContext _context;

        public CoursesController(CourseContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }
    }
}
