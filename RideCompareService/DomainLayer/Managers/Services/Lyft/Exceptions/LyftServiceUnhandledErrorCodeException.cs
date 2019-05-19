using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class LyftServiceUnhandledErrorCodeException : RideCompareBusinessBaseException
    {
        public LyftServiceUnhandledErrorCodeException()
        {
        }

        public LyftServiceUnhandledErrorCodeException(string message) : base(message)
        {
        }

        public LyftServiceUnhandledErrorCodeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LyftServiceUnhandledErrorCodeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
