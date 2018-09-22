using ExpectedObjects;
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

            Assert.Equal(1, cityDto.CityId);
            Assert.Equal("Tampere", cityDto.Name);
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

            var expectedRider = rider.ToExpectedObject();
            var expectedCity = city.ToExpectedObject();

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

            Assert.Equal(1, rideDto.RideId);
            Assert.Equal(time, rideDto.HappensOn);
            expectedRider.ShouldEqual(rideDto.OrganizedBy);
            Assert.Equal(1234545.5M, rideDto.LocationLatitude);
            Assert.Equal(5432121.5M, rideDto.LocationLongitude);
            Assert.Equal("Lahdesjärven ABC", rideDto.LocationName);
            expectedCity.ShouldEqual(rideDto.LocationCity);
            Assert.Equal("Yksikkötesti", rideDto.Description);
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

            var expectedRider = rider.ToExpectedObject();

            var riderDto = rider.Adapt<RiderDTO>();

            expectedRider.ShouldEqual(riderDto);
        }
    }
}
