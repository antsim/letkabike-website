using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LetkaBike.Core.Data
{
    public class LetkaContext : IdentityDbContext<Rider, IdentityRole, string>
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
	            .Property(e => e.LocationLatitude)
	            .HasColumnType("DECIMAL(10, 8)");
	        
	        modelBuilder.Entity<Ride>()
		        .Property(e => e.LocationLongitude)
		        .HasColumnType("DECIMAL(11, 8)");

	        modelBuilder.Entity<Ride>()
                .HasKey(e => e.RideId);

	        modelBuilder.Entity<Rider>()
		        .HasKey(e => e.RiderId);

            modelBuilder.Entity<RiderRide>()
                .HasKey(e => new {e.RiderId, e.RideId});

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options == null)
            {
	            //optionsBuilder.UseSqlServer(@"Server=(local);Database=LetkaBike;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=LetkaBike;Trusted_Connection=True;");
            }
        }
    }
}
