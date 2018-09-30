using LetkaBike.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LetkaBike.API.Controllers
{
    [Produces("application/json")]
    [Route("api/1.0/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(IUserService userService)
        {

        }
    }
}