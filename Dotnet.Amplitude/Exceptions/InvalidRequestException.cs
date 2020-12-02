using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text.Json;
using Dotnet.Amplitude.Enums;

namespace Dotnet.Amplitude.Exceptions
{
    [Serializable]
    public class InvalidRequestException : Exception, IAmplitudeException
    {
        public ResponseCode Code { get; set; }
        public string ErrorMessage { get; set; }
        public string MissingField { get; set; }
        public IDictionary<string, object> EventsWithInvalidFields { get; set; }
        public IDictionary<string, object> EventsWithMissingFields { get; set; }
        override public string Message { get { return ErrorMessage; } }

        public InvalidRequestException()
        {
            Code = ResponseCode.InvalidRequest;
        }

        public InvalidRequestException(string message)
            : base(message)
        {
            Code = ResponseCode.InvalidRequest;
            ErrorMessage = message;
        }

        public InvalidRequestException(string message, Exception inner)
            : base(message, inner)
        {
            Code = ResponseCode.InvalidRequest;
            ErrorMessage = message;
        }

        protected InvalidRequestException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
            if (info != null)
            {
                Code = Enum.Parse<ResponseCode>(info.GetInt32("code").ToString());
                ErrorMessage = info.GetString("error");
                MissingField = info.GetString("missing_field");
                EventsWithInvalidFields = JsonSerializer.Deserialize<Dictionary<string, object>>(
                    info.GetString("events_with_invalid_fields"));
                EventsWithMissingFields = JsonSerializer.Deserialize<Dictionary<string, object>>(
                    info.GetString("events_with_missing_fields"));
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
                info.AddValue("missing_field", MissingField);
                info.AddValue("events_with_invalid_fields", EventsWithInvalidFields);
                info.AddValue("events_with_missing_fields", EventsWithMissingFields);
            }
        }
    }
}
