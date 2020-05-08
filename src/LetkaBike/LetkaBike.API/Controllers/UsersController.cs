using System.Threading.Tasks;
using LetkaBike.API.Models;
using LetkaBike.Core.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetkaBike.API.Controllers
{
    [Produces("application/json")]
    [Route("api/1.0/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            var request = new RegisterUserRequest
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };
            var response = await _mediator.Send(request);
            return new JsonResult(response);
        }
    }
}