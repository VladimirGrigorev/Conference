using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ConfService.ServiceException
{
    public class ObjectException : Exception
    {
        public object Body { get; private set; }

        public ObjectException(object body)
        {
            Body = body;
        }

        protected ObjectException(SerializationInfo info, StreamingContext context, object body) : base(info, context)
        {
            Body = body;
        }

        public ObjectException(string message, object body) : base(message)
        {
            Body = body;
        }

        public ObjectException(string message, Exception innerException, object body) : base(message, innerException)
        {
            Body = body;
        }
    }
}
