using LetkaBike.Core.Data;
using System.Collections.Generic;
using LetkaBike.Core.Models;

namespace LetkaBike.Core.Services
{
    public interface ICityService
    {
        IEnumerable<CityDTO> GetAll();
        void Create(CityDTO city);
        void Delete(CityDTO city);
        void Update(CityDTO city);
    }
}
