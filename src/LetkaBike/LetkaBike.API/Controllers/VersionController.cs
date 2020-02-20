using System.Reflection;
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

        [Route("demo")]
        public IActionResult GetDemo()
        {
            return new JsonResult("Tampere.NET!!!");
        }
    }
}