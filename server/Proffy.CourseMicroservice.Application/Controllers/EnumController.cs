using Microsoft.AspNetCore.Mvc;
using Proffy.ClassMicroservice.Application.Extensions;
using Proffy.CourseMicroservice.Application;

namespace Proffy.ClassMicroservice.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        [HttpGet]
        [Route("weekday")]
        public IActionResult GetAccessLevels()
        {
            return Ok(EnumExtensions.GetValues<WeekDay>());
        }
    }
}
