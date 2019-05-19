using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RideCompareService.DomainLayer.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class RideCompareBusinessBaseException : RideCompareBaseException
    {
        public RideCompareBusinessBaseException()
        {
        }

        public RideCompareBusinessBaseException(string message) : base(message)
        {
        }

        public RideCompareBusinessBaseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RideCompareBusinessBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
