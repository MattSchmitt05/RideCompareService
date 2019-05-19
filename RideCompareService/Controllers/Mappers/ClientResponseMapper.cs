using RideCompareService.Controllers.ResourceModels;
using RideCompareService.DomainLayer.Models;

namespace RideCompareService.Controllers.Mappers
{
    internal static class ClientResponseMapper
    {
        public static ClientResponseResource MapFrom(RideCompareResponse response)
        {
            return new ClientResponseResource
            {
                ResultCode = "OK",
                ResultMessage = "Successful service call",
                BestRide = response
            };
        }
    }
}
