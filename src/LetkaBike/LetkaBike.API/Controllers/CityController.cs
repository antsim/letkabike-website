using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LetkaBike.Core.Services;
using LetkaBike.Core.Data;

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