using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Uber.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class UberGatewayInvalidRideTypesException : RideCompareBusinessBaseException
    {
        public UberGatewayInvalidRideTypesException()
        {
        }

        public UberGatewayInvalidRideTypesException(string message) : base(message)
        {
        }

        public UberGatewayInvalidRideTypesException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UberGatewayInvalidRideTypesException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
