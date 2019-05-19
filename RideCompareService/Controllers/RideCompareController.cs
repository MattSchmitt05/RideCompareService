using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RideCompareService.Controllers.Mappers;
using RideCompareService.Controllers.ResourceModels;
using RideCompareService.Controllers.Validators;
using RideCompareService.DomainLayer;
using RideCompareService.DomainLayer.Models;

namespace RideCompareService.Controllers
{
    [Route("api/[controller]")]
    public class RideCompareController : ControllerBase
    {
        // POST api/ridecompare
        [HttpPost]
        public async Task<ClientResponseResource> Post([FromBody]ClientRequestResource clientRequest)
        {
            var rideCompareRequest = RideCompareRequestMapper.MapFrom(clientRequest);

            RideCompareRequestValidator.Validate(rideCompareRequest);

            var rideCompareResponse = await GetBestRide(rideCompareRequest);

            return ClientResponseMapper.MapFrom(rideCompareResponse);
        }

        protected virtual async Task<RideCompareResponse> GetBestRide(RideCompareRequest rideCompareRequest)
        {
            using (var domainFacade = new DomainFacade())
            {
                return await domainFacade.GetBestRide(rideCompareRequest);
            }
        }
    }
}
