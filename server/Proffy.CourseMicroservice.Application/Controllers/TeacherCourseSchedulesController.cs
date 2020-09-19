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
    public class TeacherCourseSchedulesController : ControllerBase
    {
        private readonly CourseContext _context;

        public TeacherCourseSchedulesController(CourseContext context)
        {
            _context = context;
        }

        // GET: api/TeacherCoursesSchedules/UserId
        // Returns all schedules of the user / teacher
        [HttpGet("{teacherCourseId}")]
        public IQueryable<TeacherCourseSchedule> Get(Guid teacherCourseId)
        {
            var TeacherCourseSchedules = _context.TeacherCourseSchedules
                         .Where(t => t.TeacherCourseId == teacherCourseId)
                         .Select(t => t);

            return TeacherCourseSchedules;
        }

        // PUT: api/TeacherCourseSchedules/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacherCourseSchedule(Guid id, TeacherCourseSchedule teacherCourseSchedule)
        {
            if (id != teacherCourseSchedule.Id)
            {
                return BadRequest("Não foi possível atualizar. Por favor, faça login novamente.");
            }

            _context.Entry(teacherCourseSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound("Ops... Ocorreu algum erro e não foi possível atualizar a informação.");
            }

            return StatusCode(201, "Horário atualizado com sucesso!");
        }

        // POST: api/TeacherCourseSchedules
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TeacherCourseSchedule>> PostTeacherCourseSchedule(TeacherCourseSchedule teacherCourseSchedule)
        {
            _context.TeacherCourseSchedules.Add(teacherCourseSchedule);
            await _context.SaveChangesAsync();

            return StatusCode(201, new { teacherCourseSchedule.Id, message = "Horário cadastrado com sucesso!" }) ;
        }
    }
}
