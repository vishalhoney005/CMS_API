using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.Exceptions
{
    public class AllException : Exception
    {
        public AllException()
        {
        }

        public AllException(string? message) : base(message)
        {
        }

        public AllException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AllException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
