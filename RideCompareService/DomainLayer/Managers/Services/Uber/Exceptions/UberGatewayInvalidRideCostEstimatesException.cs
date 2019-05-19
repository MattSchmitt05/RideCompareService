using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Uber.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class UberGatewayInvalidRideCostEstimatesException : RideCompareBusinessBaseException
    {
        public UberGatewayInvalidRideCostEstimatesException()
        {
        }

        public UberGatewayInvalidRideCostEstimatesException(string message) : base(message)
        {
        }

        public UberGatewayInvalidRideCostEstimatesException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UberGatewayInvalidRideCostEstimatesException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
