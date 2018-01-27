using LetkaBike.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LetkaBike.Core.Services
{
    public interface ICityService
    {
        IEnumerable<City> GetAll();
        void Create(City city);
        void Delete(City city);
        void Update(City city);
    }
}
