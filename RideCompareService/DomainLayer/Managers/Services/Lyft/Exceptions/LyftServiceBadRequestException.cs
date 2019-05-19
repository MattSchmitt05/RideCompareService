using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class LyftServiceBadRequestException : RideCompareBusinessBaseException
    {
        public LyftServiceBadRequestException()
        {
        }

        public LyftServiceBadRequestException(string message) : base(message)
        {
        }

        public LyftServiceBadRequestException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LyftServiceBadRequestException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
