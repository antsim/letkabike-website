using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace LetkaBike.Core.Data
{
    public class LetkaContext : DbContext
    {
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Rider> Riders { get; set; }
        public virtual DbSet<Ride> Rides { get; set; }

        private readonly DbContextOptions<LetkaContext> _options;

        public LetkaContext(DbContextOptions<LetkaContext> options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasKey(e => e.CityId);

            modelBuilder.Entity<Ride>()
                .HasKey(e => e.RideId);

            modelBuilder.Entity<Rider>()
                .HasKey(e => e.RiderId);

            modelBuilder.Entity<RiderRide>()
                .HasKey(e => new {e.RiderId, e.RideId});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options == null)
            {
                optionsBuilder.UseSqlServer(@"Server=(local);Database=LetkaBike;Trusted_Connection=True;");
            }
        }
    }
}
