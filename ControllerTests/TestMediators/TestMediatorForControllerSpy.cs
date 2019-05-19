using System;
using System.Diagnostics.CodeAnalysis;
using RideCompareService.Controllers.ResourceModels;
using RideCompareService.DomainLayer.Models;

namespace ControllerTests.TestMediators
{
    [ExcludeFromCodeCoverage]
    internal static class TestMediatorForControllerSpy
    {
        public static ClientRequestResource ValidClientRequestResourceMock { get => GetValidClientRequestResourceMock(); }
        public static RideCompareResponse BestRideToReturn { get; set; }
        public static Exception ExceptionToThrow { get; set; }

        public static void Reset()
        {
            BestRideToReturn = null;
            ExceptionToThrow = null;
        }

        private static ClientRequestResource GetValidClientRequestResourceMock()
        {
            return new ClientRequestResource
            {
                StartingLocation = new ClientLocation
                {
                    Latitude = "1",
                    Longitude = "-1"
                },
                DestinationLocation = new ClientLocation
                {
                    Latitude = "2",
                    Longitude = "-2"
                }
            };
        } 
    }
}
