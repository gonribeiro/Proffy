using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proffy.UserMicroservice.Domain.AggregatesModel.UserAggregate;
using Proffy.UserMicroservice.Infrastructure.DataAccess.Contexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Proffy.UserMicroservice.Application.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
        private readonly UserContext _context;

        public ConnectionsController(UserContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: api/v1/Connections/Total
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

            return CreatedAtAction("GetConnection", new { id = connection.Id }, connection);
        }
    }
}
