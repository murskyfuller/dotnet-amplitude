using System;
namespace Dotnet.Amplitude.Dtos
{
    public class SuccessSummaryResponse : IResponseBody
    {
        /// <summary>
        /// 200 success code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// The number of events ingested from the upload request.
        /// </summary>
        public int EventsIngested { get; set; }

        /// <summary>
        /// The size of the upload request payload in bytes.
        /// </summary>
        public int PayloadSizeBytes { get; set; }

        /// <summary>
        /// The time in milliseconds since epoch (Unix Timestamp) that our event servers accepted the upload request.
        /// </summary>
        public long ServerUploadTime { get; set; }
    }
}
