using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Conference.CustomException
{
    public class ConfException : Exception
    {
        public object Body { get; private set; }

        public ConfException(object body)
        {
            Body = body;
        }

        protected ConfException(SerializationInfo info, StreamingContext context, object body) : base(info, context)
        {
            Body = body;
        }

        public ConfException(string message, object body) : base(message)
        {
            Body = body;
        }

        public ConfException(string message, Exception innerException, object body) : base(message, innerException)
        {
            Body = body;
        }
    }
}
