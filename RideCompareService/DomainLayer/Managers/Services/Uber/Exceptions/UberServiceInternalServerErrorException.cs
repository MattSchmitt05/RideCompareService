using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Uber.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class UberServiceInternalServerErrorException : RideCompareTechnicalBaseException
    {
        public UberServiceInternalServerErrorException()
        {
        }

        public UberServiceInternalServerErrorException(string message) : base(message)
        {
        }

        public UberServiceInternalServerErrorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UberServiceInternalServerErrorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
