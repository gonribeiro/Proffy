using Domain.Model.AggregatesModel.CourseAggregate;
using Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.WebApi.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeacherCoursesController : ControllerBase
    {
        private readonly ProffyContext _context;

        public TeacherCoursesController(ProffyContext context)
        {
            _context = context;
        }

        // GET: api/TeacherCourses/5
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
                return BadRequest(new { message = "Não foi possível atualizar. Por favor, faça login novamente." });
            }

            _context.Entry(teacherCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound(new { message = "Ops... Ocorreu algum erro e não foi possível atualizar a informação." });
            }

            return StatusCode(201, new { message = "Aula atualizada com sucesso!" });
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
