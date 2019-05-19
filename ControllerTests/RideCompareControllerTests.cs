using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using ControllerTests.Spies;
using ControllerTests.TestMediators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RideCompareService.DomainLayer.Enums;
using RideCompareService.DomainLayer.Exceptions;
using RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions;
using RideCompareService.DomainLayer.Models;

namespace ControllerTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RideCompareControllerTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            TestMediatorForControllerSpy.Reset();
        }

        private RideCompareResponse GetExpectedBestRide() => new RideCompareResponse(nameof(RideShareProvider.Lyft), 10.0M, 2);

        [TestMethod]
        [TestCategory("Controller Test")]
        public async Task RideCompareController_GetBestRide_WhenNoExceptionThrownByDomainLayer_ShouldReturnExpectedBestRide()
        {
            // Arrange
            var expectedBestRide = GetExpectedBestRide();
            TestMediatorForControllerSpy.BestRideToReturn = expectedBestRide;
            var rideCompareControllerSpy = new RideCompareControllerSpy();

            // Act
            var results = await rideCompareControllerSpy.Post(TestMediatorForControllerSpy.ValidClientRequestResourceMock);

            // Assert
            var actualBestRide = results.BestRide;
            AssertAreEqual(expectedBestRide, actualBestRide);
        }

        [TestMethod]
        [TestCategory("Controller Test")]
        public async Task RideCompareController_GetBestRide_WhenABusinessExceptionIsThrownByDomainLayer_ShouldReturnExceptionDetails()
        {
            // Arrange
            TestMediatorForControllerSpy.ExceptionToThrow = new RideCompareNoBestRideFoundException("Testing - Not relevant exception message and type.");
            var rideCompareControllerSpy = new RideCompareControllerSpy();

            try
            {
                // Act
                await rideCompareControllerSpy.Post(TestMediatorForControllerSpy.ValidClientRequestResourceMock);
                Assert.Fail($"Expected a {nameof(RideCompareNoBestRideFoundException)} to be thrown by the domain layer, but none was thrown.");
            }
            catch (RideCompareNoBestRideFoundException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, "Testing", $"Expected the Exception Message to contain \"Testing\", but Actual was \"{actualExceptionMessage}\".");
            }
        }

        [TestMethod]
        [TestCategory("Controller Test")]
        public async Task RideCompareController_GetBestRide_WhenATechnicalExceptionIsThrownByDomainLayer_ShouldReturnExceptionDetails()
        {
            // Arrange
            TestMediatorForControllerSpy.ExceptionToThrow = new LyftServiceInternalServerErrorException("Testing - Not relevant exception message and type.");
            var rideCompareControllerSpy = new RideCompareControllerSpy();

            try
            {
                // Act
                await rideCompareControllerSpy.Post(TestMediatorForControllerSpy.ValidClientRequestResourceMock);
                Assert.Fail($"Expected a {nameof(LyftServiceInternalServerErrorException)} to be thrown by the domain layer, but none was thrown.");
            }
            catch (LyftServiceInternalServerErrorException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, "Testing", $"Expected the Exception Message to contain \"Testing\", but Actual was \"{actualExceptionMessage}\".");
            }
        }

        private void AssertAreEqual(RideCompareResponse expectedRideCompareResponse, RideCompareResponse actualRideCompareResponse)
        {
            var errorMessages = new StringBuilder();

            if (!expectedRideCompareResponse.Equals(actualRideCompareResponse))
            {
                errorMessages.Append($"Did not get a Matching Actual Ride Compare Response for Expected Best Ride: RideShareProvider: {expectedRideCompareResponse.RideShareProvider}, RideCost: {expectedRideCompareResponse.RideCost}, RideEta: {expectedRideCompareResponse.RideEta}\t");
            }

            if (errorMessages.Length > 0)
            {
                throw new AssertFailedException(errorMessages.ToString());
            }
        }
    }
}
