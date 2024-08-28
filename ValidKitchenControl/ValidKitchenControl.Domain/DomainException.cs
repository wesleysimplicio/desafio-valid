using System;
using System.Runtime.Serialization;

namespace ValidKitchenControl.Domain
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

