using Microsoft.AspNetCore.Mvc;
using LetkaBike.Core.Services;

namespace LetkaBike.API.Controllers
{
    [Produces("application/json")]
    [Route("api/1.0/[controller]")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
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