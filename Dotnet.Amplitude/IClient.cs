using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Dotnet.Amplitude.Services;

namespace Dotnet.Amplitude
{
    public interface IClient : IHttpApiService
    {
        /// <summary>
        /// The HttpClient that will be used to make calls.
        /// </summary>
        HttpClient HttpClient { get; }

        /// <summary>
        /// Serializes and posts the object as a json body to the provided path. Throws any specific client exceptions
        /// for error response bodies.
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="path"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<TRes> PostAsJsonAsync<TRes>(string path, object body = null);

        /// <summary>
        /// Calls .ReadAsAsync<TRes> extension on the provided content.
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="content"></param>
        /// <param name="serializerOptions"></param>
        /// <returns></returns>
        Task<TRes> ReadAsAsync<TRes>(HttpContent content, JsonSerializerOptions serializerOptions);

        /// <summary>
        /// Calls .PostAsync extension on the Client's HttpClient
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    }
}
