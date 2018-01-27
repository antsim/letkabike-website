using LetkaBike.Core.Data;
using LetkaBike.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LetkaBike.Core.Services
{
    public class CityService : ICityService
    {
        private IRepository<City> _cityRepository;

        public CityService(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public void Create(City city)
        {
            _cityRepository.Create(city);
        }

        public void Delete(City city)
        {
            _cityRepository.Delete(city);
        }

        public IEnumerable<City> GetAll()
        {
            return _cityRepository.GetAll();
        }

        public void Update(City city)
        {
            _cityRepository.Update(city);
        }
    }
}
