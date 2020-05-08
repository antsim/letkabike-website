using System.Collections.Generic;
using LetkaBike.Core.Models.Items;

namespace LetkaBike.Core.Models.Responses
{
    public class GetCitiesResponse
    {
        public List<CityItem> Cities { get; set; }
    }
}