using System;
using System.Runtime.Serialization;
using Dotnet.Amplitude.Enums;

namespace Dotnet.Amplitude.Exceptions
{
    [Serializable]
    public class PayloadTooLargeException : Exception, IAmplitudeException
    {
        public ResponseCode Code { get; set; }
        public string ErrorMessage { get; set; }
        override public string Message { get { return ErrorMessage;  } }

        public PayloadTooLargeException()
        {
            Code = ResponseCode.PayloadTooLarge;
        }

        public PayloadTooLargeException(string message)
            : base(message)
        {
            Code = ResponseCode.PayloadTooLarge;
            ErrorMessage = message;
        }

        public PayloadTooLargeException(string message, Exception inner)
            : base(message, inner)
        {
            Code = ResponseCode.PayloadTooLarge;
            ErrorMessage = message;
        }

        protected PayloadTooLargeException(SerializationInfo info,
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
