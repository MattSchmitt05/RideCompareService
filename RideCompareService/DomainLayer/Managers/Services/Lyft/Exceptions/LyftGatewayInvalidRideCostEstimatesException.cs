using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class LyftGatewayInvalidRideCostEstimatesException : RideCompareBusinessBaseException
    {
        public LyftGatewayInvalidRideCostEstimatesException()
        {
        }

        public LyftGatewayInvalidRideCostEstimatesException(string message) : base(message)
        {
        }

        public LyftGatewayInvalidRideCostEstimatesException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LyftGatewayInvalidRideCostEstimatesException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
