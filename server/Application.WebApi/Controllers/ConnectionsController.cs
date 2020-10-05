using Domain.Model.AggregatesModel.RateAggregate;
using Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Application.WebApi.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
        private readonly ProffyContext _context;

        public ConnectionsController(ProffyContext context)
        {
            _context = context;
        }

        // GET: api/Connections
        [HttpGet("Total")]
        public ActionResult<int> Get()
        {
            var query = (from c in _context.Connections.ToList()
                         select c).ToList();

            var total = query.Count();

            return total;
        }

        // POST: api/Connections
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Connection>> PostConnection(Connection connection)
        {
            _context.Connections.Add(connection);
            await _context.SaveChangesAsync();

            return StatusCode(201, new { message = "Aluno clicou no whatsapp do professor!" });
        }
    }
}
