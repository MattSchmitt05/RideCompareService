using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class LyftGatewayInvalidAccessTokenException : RideCompareBusinessBaseException
    {
        public LyftGatewayInvalidAccessTokenException()
        {
        }

        public LyftGatewayInvalidAccessTokenException(string message) : base(message)
        {
        }

        public LyftGatewayInvalidAccessTokenException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LyftGatewayInvalidAccessTokenException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
