using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proffy.CourseMicroservice.Domain.AggregatesModel.CourseAggregate;
using Proffy.CourseMicroservice.Infrastructure.DataAccess.Contexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Proffy.CourseMicroservice.Application.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeacherCoursesController : ControllerBase
    {
        private readonly CourseContext _context;

        public TeacherCoursesController(CourseContext context)
        {
            _context = context;
        }

        // Return researched course in proffy/study
        [AllowAnonymous]
        [HttpGet("search/{CourseId},{weekday},{from}")]
        public IQueryable<TeacherCourse> Get(Guid CourseId, int weekday, int from)
        {
            var TeacherCourse = _context.TeacherCourses
                .Where(t => t.CourseId == CourseId)
                .Include(s => s.TeacherCourseSchedules)
                .Where(w => w.TeacherCourseSchedules.Any(w => w.WeekDay == weekday))
                .Where(w => w.TeacherCourseSchedules.Any(w => w.From == from))
                .Select(t => t);

            return TeacherCourse;
        }

        // GET: api/TeacherCourses/UserId
        // Returns all Courses of the user / teacher
        [HttpGet("{userId}")]
        public IQueryable<TeacherCourse> Get(Guid userId)
        {
            var TeacherCourse = _context.TeacherCourses
                         .Where(t => t.UserId == userId)
                         .Select(t => t);

            return TeacherCourse;
        }

        // PUT: api/TeacherCourses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacherCourse(Guid id, TeacherCourse teacherCourse)
        {
            if (id != teacherCourse.Id)
            {
                return BadRequest("Não foi possível atualizar. Por favor, faça login novamente.");
            }

            _context.Entry(teacherCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound("Ops... Ocorreu algum erro e não foi possível atualizar a informação.");
            }

            return StatusCode(201, "Aula atualizada com sucesso!");
        }

        // POST: api/TeacherCourses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TeacherCourse>> PostTeacherCourse(TeacherCourse teacherCourse)
        {
            teacherCourse.Actived = false;

            _context.TeacherCourses.Add(teacherCourse);

            await _context.SaveChangesAsync();

            return StatusCode(201, new { teacherCourse.Id, message = "Uhul! Agora cadastre seu horário disponível para a aula!" });
        }
    }
}
