using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dotnet.Amplitude.Dtos
{
    public class Event : IEventIdentifierMetadata, ILocationMetadata, IPurchaseMetadata, IDeviceMetadata, IDeviceIdentifierMetadata
    {
        /// <summary>
        /// A readable ID specified by you. Must have a minimum length of 5 characters. Required unless device_id is present
        /// </summary>
        [Required]
        public string UserId { get; set; }

        // Device identifier metadata
        [Required]
        public string DeviceId { get; set; }

        public string Ip { get; set; }

        public string Idfa { get; set; }

        public string Idfv { get; set; }

        public string Adid { get; set; }

        public string AndroidId { get; set; }

        // Event identifier metadata
        [Required]
        public string EventType { get; set; }

        public int? EventId { get; set; }

        public long? SessionId { get; set; }

        public string InsertId { get; set; }

        public long? Time { get; set; }

        /// <summary>
        /// A dictionary of key-value pairs that represent additional data to be sent along with the event. You can store property values in an array. Date values are transformed into string values. Object depth may not exceed 40 layers.
        /// </summary>
        public IDictionary<string, object> EventProperties { get; set; }

        /// <summary>
        /// A dictionary of key-value pairs that represent additional data tied to the user. You can store property values in an array. Date values are transformed into string values. Object depth may not exceed 40 layers.
        /// </summary>
        public IDictionary<string, object> UserProperties { get; set; }

        /// <summary>
        /// This feature is only available to Enterprise customers who have purchased the Accounts add-on. This field adds a dictionary of key-value pairs that represent groups of users to the event as an event-level group. You can only track up to 5 groups.
        /// </summary>
        public IDictionary<string, object> Groups { get; set; }

        // Device metadata
        public string AppVersion { get; set; }

        public string Platform { get; set; }

        public string OsName { get; set; }

        public string OsVersion { get; set; }

        public string DeviceBrand { get; set; }

        public string DeviceManufacturer { get; set; }

        public string DeviceModel { get; set; }

        public string Carrier { get; set; }

        // Location metadata
        public string Country { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string Dma { get; set; }

        public float? LocationLat { get; set; }

        public float? LocationLong { get; set; }

        public string Language { get; set; }

        // Product metadata
        public float? Price { get; set; }

        public int? Quantity { get; set; }

        public float? Revenue { get; set; }

        public string ProductId { get; set; }

        public string RevenueType { get; set; }
    }
}
