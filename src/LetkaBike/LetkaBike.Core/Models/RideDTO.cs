using System;
using LetkaBike.Core.Data;

namespace LetkaBike.Core.Models
{
    public class RideDTO
    {
        public int RideId { get; set; }
        public DateTimeOffset HappensOn { get; set; }
        public Rider OrganizedBy { get; set; }
        public decimal LocationLatitude { get; set; }
        public decimal LocationLongitude { get; set; }
        public string LocationName { get; set; }
        public City LocationCity { get; set; }
        public string Description { get; set; }
    }
}