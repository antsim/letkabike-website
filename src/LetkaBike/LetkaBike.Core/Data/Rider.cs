using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LetkaBike.Core.Data
{
    public partial class Rider : IdentityUser
    {
        public Rider()
        {
            Rides = new HashSet<RiderRide>();    
        }

        public int RiderId { get; set; }

        public virtual ICollection<RiderRide> Rides { get; set; }
    }
}