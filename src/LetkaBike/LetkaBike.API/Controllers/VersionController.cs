using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetkaBike.API.Controllers
{
    [Produces("application/json")]
    [Route("api/1.0/[controller]")]
    [ApiController]
    public class VersionController : Controller
    {
        public IActionResult Get()
        {
            return new JsonResult(Assembly.GetEntryAssembly()?.GetName().Version);
        }

        [HttpGet("demo")]
        public IActionResult GetWelcomeDemoPeople()
        {
	        return Ok("Tampere tech guild demo");
        }

        [Authorize]
        [HttpGet("authorized")]
        public IActionResult GetAuthenticated()
        {
            return new JsonResult(Assembly.GetEntryAssembly()?.GetName().Version);
        }
    }
}