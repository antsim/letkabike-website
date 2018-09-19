using LetkaBike.Core.Data;
using LetkaBike.Core.Repository;
using System.Collections.Generic;
using LetkaBike.Core.Models;
using Mapster;

namespace LetkaBike.Core.Services
{
    public class CityService : ICityService
    {
        private readonly IRepository<City> _cityRepository;

        public CityService(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public void Create(CityDTO city)
        {
            _cityRepository.Create(city.Adapt<City>());
        }

        public void Delete(CityDTO city)
        {
            _cityRepository.Delete(city.Adapt<City>());
        }

        public IEnumerable<CityDTO> GetAll()
        {
            return _cityRepository.GetAll().Adapt<IEnumerable<CityDTO>>();
        }

        public void Update(CityDTO city)
        {
            _cityRepository.Update(city.Adapt<City>());
        }
    }
}
