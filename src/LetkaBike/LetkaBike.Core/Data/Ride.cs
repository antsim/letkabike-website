using System;
using System.Collections;
using System.Collections.Generic;

namespace LetkaBike.Core.Data
{
    public partial class Ride
    {
        public Ride()
        {
            Riders = new HashSet<RiderRide>();    
        }

        public int RideId { get; set; }
        public DateTimeOffset HappensOn { get; set; }
        public Rider OrganizedBy { get; set; }
        public decimal LocationLatitude { get; set; }
        public decimal LocationLongitude { get; set; }
        public string LocationName { get; set; }
        public City LocationCity { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RiderRide> Riders { get; set; }
    }
}