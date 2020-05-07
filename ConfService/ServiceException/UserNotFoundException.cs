using System;
using System.Runtime.Serialization;

namespace ConfService.ServiceException
{
    public class UserNotFoundException: Exception
    {
        public object Body { get; private set; }
        public UserNotFoundException(object body)
        {
            Body = body;
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context, object body) : base(info, context)
        {
            Body = body;
        }

        public UserNotFoundException(string message, object body) : base(message)
        {
            Body = body;
        }

        public UserNotFoundException(string message, Exception innerException, object body) : base(message, innerException)
        {
            Body = body;
        }

    }
}