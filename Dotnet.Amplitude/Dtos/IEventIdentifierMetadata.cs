namespace Dotnet.Amplitude.Dtos
{
    public interface IEventIdentifierMetadata
    {
        /// <summary>
        /// (Optional) An incrementing counter to distinguish events with the same user_id and timestamp from each other. We recommend you send an event_id, increasing over time, especially if you expect events to occur simultanenously.
        /// </summary>
        int? EventId { get; set; }

        /// <summary>
        /// (Optional) The start time of the session in milliseconds since epoch (Unix Timestamp), necessary if you want to associate events with a particular system. A session_id of -1 is the same as no session_id specified.
        /// </summary>
        long? SessionId { get; set; }

        /// <summary>
        /// (Optional) A unique identifier for the event. We will deduplicate subsequent events sent with an insert_id we have already seen before within the past 7 days. We recommend generation a UUID or using some combination of device_id, user_id, event_type, event_id, and time.
        /// </summary>
        string InsertId { get; set; }

        /// <summary>
        /// The timestamp of the event in milliseconds since epoch. If time is not sent with the event, it will be set to the request upload time.
        /// </summary>
        long? Time { get; set; }
    }
}
