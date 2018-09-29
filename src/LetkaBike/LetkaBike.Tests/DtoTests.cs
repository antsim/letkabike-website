using LetkaBike.Core.Data;
using LetkaBike.Core.Models;
using Mapster;
using System;
using Xunit;

namespace LetkaBike.Tests
{
    public class DtoTests
    {
        [Fact]
        public void CityDtoTest_AllFieldsAreMapped()
        {
            var city = new City
            {
                CityId = 1,
                Name = "Tampere"
            };

            var cityDto = city.Adapt<CityDTO>();

            Assert.Equal(city.CityId, cityDto.CityId);
            Assert.Equal(city.Name, cityDto.Name);
        }

        [Fact]
        public void RideDtoTest_AllFieldsAreMapped()
        {
            var city = new City { CityId = 1, Name = "Tampere" };
            var time = DateTimeOffset.Now.AddDays(1);

            var rider = new Rider
            {
                RiderId = 1,
                AccessFailedCount = 0,
                Email = "unit@test.com",
                EmailConfirmed = false,
                LockoutEnabled = false,
                LockoutEnd = DateTimeOffset.Now,
                NormalizedEmail = "unit@test.com",
                NormalizedUserName = "unittest",
                PasswordHash = "hash",
                PhoneNumber = "0501001001",
                PhoneNumberConfirmed = false,
                SecurityStamp = "stamp",
                TwoFactorEnabled = false,
                UserName = "unittest"
            };

            var ride = new Ride
            {
                RideId = 1,
                HappensOn = time,
                OrganizedBy = rider,
                LocationLatitude = 1234545.5M,
                LocationLongitude = 5432121.5M,
                LocationName = "Lahdesjärven ABC",
                LocationCity = city,
                Description = "Yksikkötesti"
            };

            var rideDto = ride.Adapt<RideDTO>();

            Assert.Equal(ride.RideId, rideDto.RideId);
            Assert.Equal(ride.HappensOn, rideDto.HappensOn);
            Assert.IsType<Rider>(rideDto.OrganizedBy);
            Assert.Equal(ride.LocationLatitude, rideDto.LocationLatitude);
            Assert.Equal(ride.LocationLongitude, rideDto.LocationLongitude);
            Assert.Equal(ride.LocationName, rideDto.LocationName);
            Assert.IsType<City>(rideDto.LocationCity);
            Assert.Equal(ride.Description, rideDto.Description);
        }

        [Fact]
        public void RiderDtoTest_AllFieldsAreMapped()
        {
            var now = DateTimeOffset.Now;

            var rider = new Rider
            {
                RiderId = 1,
                AccessFailedCount = 0,
                Email = "unit@test.com",
                EmailConfirmed = false,
                LockoutEnabled = false,
                LockoutEnd = now,
                NormalizedEmail = "unit@test.com",
                NormalizedUserName = "unittest",
                PasswordHash = "hash",
                PhoneNumber = "0501001001",
                PhoneNumberConfirmed = false,
                SecurityStamp = "stamp",
                TwoFactorEnabled = false,
                UserName = "unittest"
            };

            var riderDto = rider.Adapt<RiderDTO>();

            Assert.Equal(rider.RiderId, riderDto.RiderId);
            Assert.Equal(rider.AccessFailedCount, riderDto.AccessFailedCount);
            Assert.Equal(rider.Email, riderDto.Email);
            Assert.Equal(rider.EmailConfirmed, riderDto.EmailConfirmed);
            Assert.Equal(rider.LockoutEnabled, riderDto.LockoutEnabled);
            Assert.Equal(rider.LockoutEnd, riderDto.LockoutEnd);
            Assert.Equal(rider.NormalizedEmail, riderDto.NormalizedEmail);
            Assert.Equal(rider.NormalizedUserName, riderDto.NormalizedUserName);
            Assert.Equal(rider.PasswordHash, riderDto.PasswordHash);
            Assert.Equal(rider.PhoneNumber, riderDto.PhoneNumber);
            Assert.Equal(rider.PhoneNumberConfirmed, riderDto.PhoneNumberConfirmed);
            Assert.Equal(rider.SecurityStamp, riderDto.SecurityStamp);
            Assert.Equal(rider.TwoFactorEnabled, riderDto.TwoFactorEnabled);
            Assert.Equal(rider.UserName, riderDto.UserName);
        }
    }
}
