using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class LyftServiceNotFoundException : RideCompareBusinessBaseException
    {
        public LyftServiceNotFoundException()
        {
        }

        public LyftServiceNotFoundException(string message) : base(message)
        {
        }

        public LyftServiceNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LyftServiceNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
