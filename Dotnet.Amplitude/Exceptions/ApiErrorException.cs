using System;
using System.Runtime.Serialization;
using Dotnet.Amplitude.Enums;

namespace Dotnet.Amplitude.Exceptions
{
    [Serializable]
    public class ApiErrorException : Exception, IAmplitudeException
    {
        public ResponseCode Code { get; set; }
        public string ErrorMessage { get; set; }
        override public string Message { get { return ErrorMessage; } }

        public ApiErrorException()
        {
            Code = ResponseCode.PayloadTooLarge;
        }

        public ApiErrorException(string message)
            : base(message)
        {
            Code = ResponseCode.PayloadTooLarge;
            ErrorMessage = message;
        }

        public ApiErrorException(string message, Exception inner)
            : base(message, inner)
        {
            Code = ResponseCode.PayloadTooLarge;
            ErrorMessage = message;
        }

        protected ApiErrorException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
            if (info != null)
            {
                Code = Enum.Parse<ResponseCode>(info.GetInt32("code").ToString());
                ErrorMessage = info.GetString("error");
            }
        }

        public override void GetObjectData(SerializationInfo info,
            StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("code", Code);
                info.AddValue("error", ErrorMessage);
            }
        }
    }
}
