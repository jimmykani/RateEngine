using Moq;
using RateEngine.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using RateEngine.Core.Model;
using RateEngine.Application.Features.Rate.Get;
using Xunit;

namespace RateEngine.UnitTest.Functions
{
   public  class GetFunctionTest
    {
        [Fact]
        public async Task Should_Returns_Rates()
        {
            var standardRateResponse = new ParkingRateResponse
            {
                Name = "Night Rate",
                Price = 6.5
            };
            var specialRateResponse = new ParkingRateResponse
            {
                Name = "Night Rate",
                Price = 24
            };
            var specialService = new Mock<ISpecialRateService>();
            var standardService = new Mock<IStandardRateService>();
            var sartTime = new DateTime(2020, 7, 11, 18, 0, 0);
            var endTime = new DateTime(2020, 7, 12, 6, 0, 0);
                specialService
                .Setup(ps => ps.Calculate(sartTime, endTime))
                .Returns(specialRateResponse);
            standardService
                .Setup(ps => ps.Calculate(sartTime, endTime))
                .Returns(standardRateResponse);
            var handler = new Handler(specialService.Object, standardService.Object);

            var result = await handler.Handle(new Query
            {
                EntryTime   = sartTime,
               ExitTime = endTime
            }, CancellationToken.None);
			
            Assert.AreEqual(result.Name,standardRateResponse.Name);
            Assert.AreEqual(result.Price,standardRateResponse.Price);
        }
        [Fact]
        public async Task Should_Returns_EarlyBirdRates()
        {
            var standardRateResponse = new ParkingRateResponse
            {
                Name = "Early Bird",
                Price = 13.00
            };
            var specialRateResponse = new ParkingRateResponse
            {
                Name = "Night Rate",
                Price = 24
            };
            var specialService = new Mock<ISpecialRateService>();
            var standardService = new Mock<IStandardRateService>();
            var sartTime = new DateTime(2020, 7, 2, 07, 0, 0);
            var endTime = new DateTime(2020, 7, 2, 16, 0, 0);
            specialService
                .Setup(ps => ps.Calculate(sartTime, endTime))
                .Returns(specialRateResponse);
            standardService
                .Setup(ps => ps.Calculate(sartTime, endTime))
                .Returns(standardRateResponse);
            var handler = new Handler(specialService.Object, standardService.Object);

            var result = await handler.Handle(new Query
            {
                EntryTime   = sartTime,
                ExitTime = endTime
            }, CancellationToken.None);
			
            Assert.AreEqual(result.Name,standardRateResponse.Name);
            Assert.AreEqual(result.Price,standardRateResponse.Price);
        }
    }
}
