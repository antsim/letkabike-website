using System.Threading.Tasks;
using LetkaBike.Core.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LetkaBike.API.Controllers
{
    [Produces("application/json")]
    [Route("api/1.0/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<JsonResult> GetCities()
        {
            var request = new GetCitiesRequest();
            var response = await _mediator.Send(request);
            return new JsonResult(response.Cities);
        }
    }
}