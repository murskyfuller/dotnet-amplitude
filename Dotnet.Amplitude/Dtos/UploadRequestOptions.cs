namespace Dotnet.Amplitude.Dtos
{
    public class UploadRequestOptions
    {
        /// <summary>
        /// Minimum permitted length for user_id & device_id fields. Default is 5.
        /// </summary>
        public int MinIdLength { get; set; }
    }
}
