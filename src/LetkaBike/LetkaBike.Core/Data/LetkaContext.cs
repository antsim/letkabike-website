using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace LetkaBike.Core.Data
{
    public class LetkaContext : DbContext
    {
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Rider> Rider { get; set; }
        public virtual DbSet<Ride> Ride { get; set; }

        private readonly DbContextOptions _options;

        public LetkaContext(DbContextOptions options) : base(options)
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
