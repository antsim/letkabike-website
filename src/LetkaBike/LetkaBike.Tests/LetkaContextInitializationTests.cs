using System.IO;
using LetkaBike.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace LetkaBike.Tests
{
    public class LetkaContextInitializationTests
    {
        public LetkaContextInitializationTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<LetkaContext>()
                .UseSqlServer(_configuration.GetConnectionString("LetkaDatabase"))
                .Options;
        }

        // to have the same Configuration object as in Startup
        private readonly IConfigurationRoot _configuration;

        // represents database's configuration
        private readonly DbContextOptions<LetkaContext> _options;

        [Fact(Skip = "Not an actual test, only to re-create the local DB")]
        public void InitializeDatabaseWithTestData()
        {
            using var context = new LetkaContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Cities.Add(new City {Name = "Helsinki"});
            context.Cities.Add(new City {Name = "Espoo"});
            context.Cities.Add(new City {Name = "Tampere"});

            context.Riders.Add(new Rider {Email = "rider@letkabike.com", UserName = "rider"});

            context.SaveChanges();

            Assert.True(true);
        }
    }
}