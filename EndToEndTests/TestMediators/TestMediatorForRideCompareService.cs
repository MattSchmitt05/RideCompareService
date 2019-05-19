using RideCompareService.Controllers.ResourceModels;
using System.Diagnostics.CodeAnalysis;

namespace EndToEndTests.TestMediators
{
    [ExcludeFromCodeCoverage]
    internal static class TestMediatorForRideCompareService
    {
        public static ClientRequestResource ValidClientRequestResourceMock { get => GetValidClientRequestResourceMock(); }
        public static ClientRequestResource InvalidClientRequestResourceMock { get => GetInvalidClientRequestResourceMock(); }

        private static ClientRequestResource GetValidClientRequestResourceMock()
        {
            return new ClientRequestResource
            {
                StartingLocation = new ClientLocation
                {
                    Latitude = "39.9784",
                    Longitude = "-86.1180"
                },
                DestinationLocation = new ClientLocation
                {
                    Latitude = "39.7684",
                    Longitude = "-86.1581"
                }
            };
        }

        private static ClientRequestResource GetInvalidClientRequestResourceMock()
        {
            return new ClientRequestResource
            {
                StartingLocation = new ClientLocation
                {
                    Latitude = "39.9784",
                    Longitude = "-86.1180"
                },
                DestinationLocation = new ClientLocation
                {
                    Latitude = "test",
                    Longitude = "test"
                }
            };
        }
    }
}
