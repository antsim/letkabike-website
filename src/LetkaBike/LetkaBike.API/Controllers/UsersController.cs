using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LetkaBike.API.Configuration;
using LetkaBike.API.Models;
using LetkaBike.Core.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace LetkaBike.API.Controllers
{
    [Produces("application/json")]
    [Route("api/1.0/[controller]")]
    [Authorize]
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
        public async Task<IActionResult> Register([FromBody] RegisterUserDto apiRequest)
        {
            var request = new RegisterUserRequest
            {
                Username = apiRequest.Username,
                Email = apiRequest.Email,
                Password = apiRequest.Password
            };
            var response = await _mediator.Send(request);
            return new JsonResult(response);
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateUserDto apiRequest)
        {
            var request = new AuthenticateUserRequest
            {
                Email = apiRequest.Email,
                Password = apiRequest.Password
            };
            
            var response = await _mediator.Send(request);

            if (response?.Rider == null)
            {
                return BadRequest(new {message = "Username or password is incorrect"});
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.AuthSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, response.Rider.Id),
                    new Claim(ClaimTypes.Name, response.Rider.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            return Ok(new
            {
                response.Rider.Id,
                response.Rider.UserName,
                Token = tokenString
            });
        }
    }
}