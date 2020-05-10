using System.IO;
using IdentityServer4.EntityFramework.Options;
using LetkaBike.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
            _operationalStoreOptions = Options.Create(new OperationalStoreOptions
            {
                ConfigureDbContext = b =>
                {
                    b.UseSqlServer(_configuration.GetConnectionString("LetkaDatabase"),
                        sql => sql.MigrationsAssembly("LetkaBike.Core"));
                },
                EnableTokenCleanup = true,
                TokenCleanupInterval = 30
            });
        }

        // to have the same Configuration object as in Startup
        private readonly IConfigurationRoot _configuration;

        // represents database's configuration
        private readonly DbContextOptions<LetkaContext> _options;
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;
        [Fact]
        public void InitializeDatabaseWithTestData()
        {
            using var context = new LetkaContext(_options, _operationalStoreOptions);
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