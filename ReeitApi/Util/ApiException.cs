using ReeitApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ReeitApi.Util
{
    public class ApiException : Exception
    {
        public ErrorCode ErrorCode { get; private set; }
        public string Name { get => ErrorCode.ToString(); }
        public string Description { get; private set; }

        public ApiException(ErrorCode errorCode, string description = null)
        {
            ErrorCode = errorCode;
            Description = description;
        }

        public ApiException(string message) : base(message) { }

        public ApiException(string message, Exception exception) : base(message, exception) { }

        public ApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
