using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Model.AggregatesModel.CourseAggregate;
using Infrastructure.Data.Contexts;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherCoursesController : ControllerBase
    {
        private readonly ProffyContext _context;

        public TeacherCoursesController(ProffyContext context)
        {
            _context = context;
        }

        // GET: api/TeacherCourses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherCourse>>> GetTeacherCourses()
        {
            return await _context.TeacherCourses.ToListAsync();
        }

        // GET: api/TeacherCourses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherCourse>> GetTeacherCourse(Guid id)
        {
            var teacherCourse = await _context.TeacherCourses.FindAsync(id);

            if (teacherCourse == null)
            {
                return NotFound();
            }

            return teacherCourse;
        }

        // PUT: api/TeacherCourses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacherCourse(Guid id, TeacherCourse teacherCourse)
        {
            if (id != teacherCourse.Id)
            {
                return BadRequest();
            }

            _context.Entry(teacherCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherCourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TeacherCourses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TeacherCourse>> PostTeacherCourse(TeacherCourse teacherCourse)
        {
            _context.TeacherCourses.Add(teacherCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacherCourse", new { id = teacherCourse.Id }, teacherCourse);
        }

        // DELETE: api/TeacherCourses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TeacherCourse>> DeleteTeacherCourse(Guid id)
        {
            var teacherCourse = await _context.TeacherCourses.FindAsync(id);
            if (teacherCourse == null)
            {
                return NotFound();
            }

            _context.TeacherCourses.Remove(teacherCourse);
            await _context.SaveChangesAsync();

            return teacherCourse;
        }

        private bool TeacherCourseExists(Guid id)
        {
            return _context.TeacherCourses.Any(e => e.Id == id);
        }
    }
}
