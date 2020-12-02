using System.Collections.Generic;

namespace Dotnet.Amplitude.Dtos
{
    public class TooManyRequestsForDeviceResponse : IErrorResponseBody
    {
        /// <summary>
        /// 429 error code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Error description.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Your app's current events per second threshold. If you exceed this rate your requests will be throttled.
        /// </summary>
        public int EpsThreshold { get; set; }

        /// <summary>
        /// A map from device_id to its current events per second rate, for all devices that exceed the app's current threshold.
        /// </summary>
        public IDictionary<string, int> ThrottledDevices { get; set; }

        /// <summary>
        /// A map from user_id to their current events per second rate, for all users that exceed the app's current threshold
        /// </summary>
        public IDictionary<string, int> ThrottledUsers { get; set; }

        /// <summary>
        /// Array of indexes in the events array indicating events whose user_id and/or device_id got throttled
        /// </summary>
        public ICollection<int> ThrottledEvents { get; set; }
    }
}
