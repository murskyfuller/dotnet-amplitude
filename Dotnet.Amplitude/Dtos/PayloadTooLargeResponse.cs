using System;
namespace Dotnet.Amplitude.Dtos
{
    public class PayloadTooLargeResponse : IErrorResponseBody
    {
        /// <summary>
        /// 413 error code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Error description.
        /// </summary>
        public string Error { get; set; }
    }
}
