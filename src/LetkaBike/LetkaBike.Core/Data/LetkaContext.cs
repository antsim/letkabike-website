using System.Collections.Generic;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LetkaBike.Core.Data
{
    public class LetkaContext : ApiAuthorizationDbContext<Rider>
    {
        private readonly DbContextOptions<LetkaContext> _options;
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;
        
        public LetkaContext(DbContextOptions<LetkaContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
            _options = options;
            _operationalStoreOptions = operationalStoreOptions;
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Rider> Riders { get; set; }
        public virtual DbSet<Ride> Rides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasKey(e => e.CityId);

            modelBuilder.Entity<Ride>()
                .Property(e => e.LocationLatitude)
                .HasColumnType("DECIMAL(10, 8)");

            modelBuilder.Entity<Ride>()
                .Property(e => e.LocationLongitude)
                .HasColumnType("DECIMAL(11, 8)");

            modelBuilder.Entity<Ride>()
                .HasKey(e => e.RideId);

            modelBuilder.Entity<Rider>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<RiderRide>()
                .HasKey(e => new {e.RiderId, e.RideId});

            modelBuilder.Entity<City>()
                .HasData(new List<City>
                {
                    new City {CityId = 1, Name = "Tampere"},
                    new City {CityId = 2, Name = "Helsinki"}
                });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options == null || !optionsBuilder.IsConfigured)
                optionsBuilder.UseInMemoryDatabase("Letka");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=LetkaBike;Trusted_Connection=True;");
        }
    }
}