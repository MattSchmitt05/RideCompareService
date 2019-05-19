using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Uber.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class UberServiceUnhandledErrorCodeException : RideCompareBusinessBaseException
    {
        public UberServiceUnhandledErrorCodeException()
        {
        }

        public UberServiceUnhandledErrorCodeException(string message) : base(message)
        {
        }

        public UberServiceUnhandledErrorCodeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UberServiceUnhandledErrorCodeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
