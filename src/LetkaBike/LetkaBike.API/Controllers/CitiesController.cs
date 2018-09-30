using Microsoft.AspNetCore.Mvc;
using LetkaBike.Core.Services;

namespace LetkaBike.API.Controllers
{
    [Produces("application/json")]
    [Route("api/1.0/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public JsonResult GetCities()
        {
            var cities = _cityService.GetAll();
            return new JsonResult(cities);
        }
    }
}