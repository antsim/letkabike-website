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

        public IEnumerable<City> GetAll()
        {
            return _cityRepository.GetAll();
        }
    }
}
