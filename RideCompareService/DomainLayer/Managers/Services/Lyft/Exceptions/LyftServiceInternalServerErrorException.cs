using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class LyftServiceInternalServerErrorException : RideCompareTechnicalBaseException
    {
        public LyftServiceInternalServerErrorException()
        {
        }

        public LyftServiceInternalServerErrorException(string message) : base(message)
        {
        }

        public LyftServiceInternalServerErrorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LyftServiceInternalServerErrorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
