using System.Threading.Tasks;
using LetkaBike.API.Configuration;
using LetkaBike.API.Models;
using LetkaBike.Core.Data;
using LetkaBike.Core.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LetkaBike.API.Controllers
{
    [Produces("application/json")]
    [Route("api/1.0/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<Rider> _signInManager;
        private readonly UserManager<Rider> _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IMediator mediator, 
            IOptions<AppOptions> options,
            SignInManager<Rider> signInManager,
            UserManager<Rider> userManager,
            ILogger<UsersController> logger
            )
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto apiRequest)
        {
            var request = new GetRiderRequest
            {
                Email = apiRequest.Email
            };

            var rider = new Rider
            {
                Email = apiRequest.Email,
                UserName = apiRequest.UserName
            };
            
            var result = await _userManager.CreateAsync(rider, apiRequest.Password);

            if (result != IdentityResult.Success)
            {
                return BadRequest();
            }
            
            await _signInManager.SignInAsync(rider, false);
            return Ok();
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login(AuthenticateUserDto apiRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(
                apiRequest.UserName, 
                apiRequest.Password, 
                true, 
                true);
            
            if (result.Succeeded)
            {
                return Ok();
            }
            
            return BadRequest();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}