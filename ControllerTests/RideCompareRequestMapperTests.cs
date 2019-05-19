using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RideCompareService.Controllers.Mappers;
using RideCompareService.Controllers.ResourceModels;
using RideCompareService.DomainLayer.Models;

namespace ControllerTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RideCompareRequestMapperTests
    {
        private const string InvalidLocationValue = "test";

        [TestMethod]
        [TestCategory("Controller Test")]
        public void RideCompareRequestMapper_MapFrom_WhenClientRequestHasInvalidStartingLocation_ShouldMapToDefaultValues()
        {
            // Arrange
            var clientRequestDataMock = new ClientRequestResource
            {
                StartingLocation = new ClientLocation { Latitude = InvalidLocationValue, Longitude = InvalidLocationValue },
                DestinationLocation = new ClientLocation { Latitude = "1", Longitude = "-1"}
            };

            var expectedRideCompareRequest = new RideCompareRequest(new RideCompareLocation(double.NaN, double.NaN), new RideCompareLocation(1, -1));

            // Act
            var actualRideCompareRequest = RideCompareRequestMapper.MapFrom(clientRequestDataMock);

            // Assert
            Assert.AreEqual(expectedRideCompareRequest.StartingLatitude, actualRideCompareRequest.StartingLatitude, $"Expected starting location latitude to be (double.NaN) but Actual was: {actualRideCompareRequest.StartingLatitude}.");
            Assert.AreEqual(expectedRideCompareRequest.StartingLongitude, actualRideCompareRequest.StartingLongitude, $"Expected starting location longitude to be (double.NaN) but Actual was: {actualRideCompareRequest.StartingLongitude}.");
        }

        [TestMethod]
        [TestCategory("Controller Test")]
        public void RideCompareRequestMapper_MapFrom_WhenClientRequestHasInvalidDestinationLocation_ShouldMapToDefaultValues()
        {
            // Arrange
            var clientRequestDataMock = new ClientRequestResource
            {
                StartingLocation = new ClientLocation { Latitude = "1", Longitude = "-1" },
                DestinationLocation = new ClientLocation { Latitude = InvalidLocationValue, Longitude = InvalidLocationValue }
            };

            var expectedRideCompareRequest = new RideCompareRequest(new RideCompareLocation(1, -1), new RideCompareLocation(double.NaN, double.NaN));
            
            // Act
            var actualRideCompareRequest = RideCompareRequestMapper.MapFrom(clientRequestDataMock);

            // Assert
            Assert.AreEqual(expectedRideCompareRequest.DestinationLatitude, actualRideCompareRequest.DestinationLatitude, $"Expected destination location latitude to be (double.NaN) but Actual was: {actualRideCompareRequest.DestinationLatitude}.");
            Assert.AreEqual(expectedRideCompareRequest.DestinationLongitude, actualRideCompareRequest.DestinationLongitude, $"Expected destination location longitude to be (double.NaN) but Actual was: {actualRideCompareRequest.DestinationLongitude}.");
        }
    }
}
