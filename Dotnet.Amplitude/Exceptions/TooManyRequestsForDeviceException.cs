using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text.Json;
using Dotnet.Amplitude.Enums;

namespace Dotnet.Amplitude.Exceptions
{
    [Serializable]
    public class TooManyRequestsForDeviceException : Exception, IAmplitudeException
    {
        public ResponseCode Code { get; set; }
        public string ErrorMessage { get; set; }
        public int EpsThreshold { get; set; }
        public IDictionary<string, int> ThrottledDevices { get; set; }
        public IDictionary<string, int> ThrottledUsers { get; set; }
        public ICollection<int> ThrottledEvents { get; set; }

        override public string Message { get { return ErrorMessage; } }

        public TooManyRequestsForDeviceException()
        {
            Code = ResponseCode.TooManyRequestsForDevice;
        }

        public TooManyRequestsForDeviceException(string message)
            : base(message)
        {
            Code = ResponseCode.TooManyRequestsForDevice;
            ErrorMessage = message;
        }

        public TooManyRequestsForDeviceException(string message, Exception inner)
            : base(message, inner)
        {
            Code = ResponseCode.TooManyRequestsForDevice;
            ErrorMessage = message;
        }

        protected TooManyRequestsForDeviceException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
            if (info != null)
            {
                Code = Enum.Parse<ResponseCode>(info.GetInt32("code").ToString());
                ErrorMessage = info.GetString("error");
                EpsThreshold = info.GetInt32("eps_threshold");
                ThrottledDevices = JsonSerializer.Deserialize<Dictionary<string, int>>(
                    info.GetString("throttled_devices"));
                ThrottledUsers = JsonSerializer.Deserialize<Dictionary<string, int>>(
                    info.GetString("throttled_users"));
                ThrottledEvents = JsonSerializer.Deserialize<List<int>>(
                    info.GetString("throttled_events"));
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
                info.AddValue("eps_threshold", EpsThreshold);
                info.AddValue("throttled_devices", ThrottledDevices);
                info.AddValue("throttled_users", ThrottledUsers);
                info.AddValue("throttled_events", ThrottledEvents);
            }
        }
    }
}
