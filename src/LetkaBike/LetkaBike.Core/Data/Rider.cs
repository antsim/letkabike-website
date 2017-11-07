using System.Collections;
using System.Collections.Generic;

namespace LetkaBike.Core.Data
{
    public partial class Rider
    {
        public Rider()
        {
            Rides = new HashSet<RiderRide>();    
        }

        public int RiderId { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }

        public virtual ICollection<RiderRide> Rides { get; set; }
    }
}