using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Uber.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class UberServiceBadRequestException : RideCompareBusinessBaseException
    {
        public UberServiceBadRequestException()
        {
        }

        public UberServiceBadRequestException(string message) : base(message)
        {
        }

        public UberServiceBadRequestException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UberServiceBadRequestException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
