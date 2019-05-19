using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Uber.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class UberServiceNotFoundException : RideCompareBusinessBaseException
    {
        public UberServiceNotFoundException()
        {
        }

        public UberServiceNotFoundException(string message) : base(message)
        {
        }

        public UberServiceNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UberServiceNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
