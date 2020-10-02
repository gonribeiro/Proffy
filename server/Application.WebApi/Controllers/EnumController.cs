using Application.WebApi.Extensions;
using Domain.Model.AggregatesModel.CourseAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebApi.Controllers
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
