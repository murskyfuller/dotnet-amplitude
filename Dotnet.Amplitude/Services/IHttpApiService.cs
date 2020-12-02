using System.Threading.Tasks;
using System.Collections.Generic;
using Dotnet.Amplitude.Dtos;

namespace Dotnet.Amplitude.Services
{
    public interface IHttpApiService
    {
        /// <summary>
        /// Sends the provided events to Amplitude via a POST request.
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        Task<SuccessSummaryResponse> UploadEvents(ICollection<Event> events);
    }
}
