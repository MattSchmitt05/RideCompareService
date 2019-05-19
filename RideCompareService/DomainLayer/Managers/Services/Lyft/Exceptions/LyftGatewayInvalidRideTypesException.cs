using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class LyftGatewayInvalidRideTypesException : RideCompareBusinessBaseException
    {
        public LyftGatewayInvalidRideTypesException()
        {
        }

        public LyftGatewayInvalidRideTypesException(string message) : base(message)
        {
        }

        public LyftGatewayInvalidRideTypesException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LyftGatewayInvalidRideTypesException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
