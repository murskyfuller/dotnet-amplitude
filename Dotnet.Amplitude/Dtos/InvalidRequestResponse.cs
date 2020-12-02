using System.Collections.Generic;

namespace Dotnet.Amplitude.Dtos
{
    public class InvalidRequestResponse : IErrorResponseBody
    {
        /// <summary>
        /// 400 error code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Error description. Possible values are Invalid request path, Missing request body, Invalid JSON request body, Request missing required field, Invalid event JSON, Invalid API key, Invalid field values on some events
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Indicates which request-level required field is missing.
        /// </summary>
        public string MissingField { get; set; }

        /// <summary>
        /// A map from field names to an array of indexes into the events array indicating which events have invalid values for those fields
        /// </summary>
        public IDictionary<string, object> EventsWithInvalidFields { get; set;}

        /// <summary>
        /// A map from field names to an array of indexes into the events array indicating which events are missing those required fields
        /// </summary>
        public IDictionary<string, object> EventsWithMissingFields { get; set; }
    }
}
