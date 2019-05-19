using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RideCompareService.DomainLayer.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class RideCompareBaseException : Exception
    {
        public RideCompareBaseException()
        {
        }

        public RideCompareBaseException(string message) : base(message)
        {
        }

        public RideCompareBaseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RideCompareBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
