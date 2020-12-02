using System.Collections.Generic;

namespace Dotnet.Amplitude.Dtos
{
    public class UploadRequestBody
    {
        public string ApiKey { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
