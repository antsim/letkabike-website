namespace LetkaBike.Core.Data
{
    public class RiderRide
    {
        public Rider Rider { get; set; }
        public int RiderId { get; set; }

        public Ride Ride { get; set; }
        public int RideId { get; set; }
    }
}