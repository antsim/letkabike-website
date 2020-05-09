using System.Threading.Tasks;
using LetkaBike.API.Configuration;
using LetkaBike.API.Models;
using LetkaBike.Core.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LetkaBike.API.Controllers
{
    [Produces("application/json")]
    [Route("api/1.0/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AppOptions _options;

        public UsersController(
            IMediator mediator, 
            IOptions<AppOptions> options
            )
        {
            _mediator = mediator;
            _options = options.Value;
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