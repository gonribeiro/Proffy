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
    public class SchedulesController : ControllerBase
    {
        private readonly ProffyContext _context;

        public SchedulesController(ProffyContext context)
        {
            _context = context;
        }

        // PUT: api/Schedules/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(Guid id, Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return BadRequest(new { message = "Não foi possível atualizar. Por favor, faça login novamente." });
            }

            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(new { message = "Ops... Ocorreu algum erro e não foi possível atualizar a informação."});
            }

            return StatusCode(201, new { message = "Horário atualizado com sucesso!" });
        }

        // POST: api/Schedules
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return StatusCode(201, new { schedule.Id, message = "Horário cadastrado com sucesso!" });
        }
    }
}
