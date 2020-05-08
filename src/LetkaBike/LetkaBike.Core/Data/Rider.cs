using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LetkaBike.Core.Data
{
    public class Rider : IdentityUser
    {
        public Rider()
        {
            Rides = new List<RiderRide>();
        }

        public int RiderId { get; set; }

        public virtual ICollection<RiderRide> Rides { get; set; }
    }
}