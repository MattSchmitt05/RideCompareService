using System;
using RideCompareService.DomainLayer.Models;

namespace RideCompareService.Controllers.ResourceModels
{
    public sealed class ClientResponseResource
    {
        public string Timestamp { get; } = DateTime.Now.ToString();
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public RideCompareResponse BestRide { get; set; }
    }
}
