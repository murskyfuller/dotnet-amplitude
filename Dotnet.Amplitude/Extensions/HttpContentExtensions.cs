using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dotnet.Amplitude
{
    public static class HttpContentExtensions
    {
        public static async Task<TOut> ReadAsAsync<TOut>(this HttpContent content, JsonSerializerOptions options = null) =>
            JsonSerializer.Deserialize<TOut>(await content.ReadAsStringAsync(), options);
    }
}