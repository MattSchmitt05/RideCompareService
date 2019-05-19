using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Uber.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class UberGatewayInvalidRideEtaEstimatesException : RideCompareBusinessBaseException
    {
        public UberGatewayInvalidRideEtaEstimatesException()
        {
        }

        public UberGatewayInvalidRideEtaEstimatesException(string message) : base(message)
        {
        }

        public UberGatewayInvalidRideEtaEstimatesException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UberGatewayInvalidRideEtaEstimatesException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
