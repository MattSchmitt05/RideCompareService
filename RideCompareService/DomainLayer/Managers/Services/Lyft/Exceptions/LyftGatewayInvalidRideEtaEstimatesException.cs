using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class LyftGatewayInvalidRideEtaEstimatesException : RideCompareBusinessBaseException
    {
        public LyftGatewayInvalidRideEtaEstimatesException()
        {
        }

        public LyftGatewayInvalidRideEtaEstimatesException(string message) : base(message)
        {
        }

        public LyftGatewayInvalidRideEtaEstimatesException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LyftGatewayInvalidRideEtaEstimatesException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
