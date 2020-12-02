using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dotnet.Amplitude.Enums;
using Dotnet.Amplitude.Dtos;
using Dotnet.Amplitude.Exceptions;

namespace Dotnet.Amplitude
{
    public class Client : IClient
    {
        public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            IgnoreNullValues = true,
            PropertyNamingPolicy = NamingPolicies.SnakeCase
        };

        private readonly ILogger<IClient> _logger;
        private readonly IOptions<AmplitudeSettings> _settings;

        public Client(HttpClient client, IOptions<AmplitudeSettings> settings, ILogger<IClient> logger)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(settings.Value?.BaseUrl)) throw new Exception("Amplitude BaseUrl required.");
            if (string.IsNullOrWhiteSpace(settings.Value?.ApiKey)) throw new Exception("Amplitude ApiKey required.");

            _logger = logger;
            _settings = settings;

            if (client.BaseAddress == null) client.BaseAddress = new Uri(settings.Value.BaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            HttpClient = client;
        }

        public HttpClient HttpClient { get; private set; }

        public Task<SuccessSummaryResponse> UploadEvents(ICollection<Event> events)
        {
            if (events == null) throw new ArgumentNullException(nameof(events));

            var body = AddAuthorization(new UploadRequestBody() { Events = events });
            return PostAsJsonAsync<SuccessSummaryResponse>("2/httpapi", body);
        }

        public virtual async Task<TRes> PostAsJsonAsync<TRes>(string path, object body = null)
        {
            _logger.LogDebug("Amplitude post body is null: {status}", body == null);
            _logger.LogDebug("Amplitude request: {baseAddress}{path}", HttpClient.BaseAddress, path);

            var serialized = JsonSerializer.SerializeToUtf8Bytes(body, SerializerOptions);
            var content = new ByteArrayContent(serialized);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await PostAsync(path, content);

            _logger.LogDebug("Amplitude response received: {isSuccess} {statusCode}", response.IsSuccessStatusCode, response.StatusCode);

            // consolidate Amplitude response error handling
            if (!response.IsSuccessStatusCode)
            {
                var error = await ReadAsAsync<IErrorResponseBody>(response.Content, SerializerOptions);
                _logger.LogError("{operation} AMPLITUDE_ERROR: {errorCode} | {errorMessage}", "PostAsJsonAsync", error.Code, error.Error);

                // Handle code
                // 400, 413, 429
                var errorCode = Enum.Parse<ResponseCode>(error.Code.ToString());
                switch (errorCode)
                {
                    case ResponseCode.InvalidRequest:
                        var invalidRequestBody = await ReadAsAsync<InvalidRequestResponse>(response.Content, SerializerOptions);
                        throw new InvalidRequestException()
                        {
                            Code = errorCode,
                            ErrorMessage = invalidRequestBody.Error,
                            MissingField = invalidRequestBody.MissingField,
                            EventsWithInvalidFields = invalidRequestBody.EventsWithInvalidFields,
                            EventsWithMissingFields = invalidRequestBody.EventsWithMissingFields,
                        };
                    case ResponseCode.PayloadTooLarge:
                        var payloadTooLargResponseBody = await ReadAsAsync<PayloadTooLargeResponse>(response.Content, SerializerOptions);
                        throw new PayloadTooLargeException()
                        {
                            Code = errorCode,
                            ErrorMessage = payloadTooLargResponseBody.Error
                        };
                    case ResponseCode.TooManyRequestsForDevice:
                        var tooManyRequestsForDeviceBody = await ReadAsAsync<TooManyRequestsForDeviceResponse>(response.Content, SerializerOptions);
                        throw new TooManyRequestsForDeviceException()
                        {
                            Code = errorCode,
                            ErrorMessage = tooManyRequestsForDeviceBody.Error,
                            EpsThreshold = tooManyRequestsForDeviceBody.EpsThreshold,
                            ThrottledDevices = tooManyRequestsForDeviceBody.ThrottledDevices,
                            ThrottledUsers = tooManyRequestsForDeviceBody.ThrottledUsers,
                            ThrottledEvents = tooManyRequestsForDeviceBody.ThrottledEvents
                        };
                    default:
                        _logger.LogWarning("Unhandled Amplitude error: {errorCode}, {errorMessage}", error.Code, error.Error);
                        throw new ApiErrorException()
                        {
                            Code = errorCode,
                            ErrorMessage = error.Error
                        };
                }
            }

            // Handle success
            return await ReadAsAsync<TRes>(response.Content, SerializerOptions);
        }

        public virtual Task<TRes> ReadAsAsync<TRes>(HttpContent content, JsonSerializerOptions serializerOptions) =>
            content.ReadAsAsync<TRes>(serializerOptions);

        public virtual Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content) => HttpClient.PostAsync(requestUri, content);

        private UploadRequestBody AddAuthorization(UploadRequestBody request)
        {
            request.ApiKey = _settings.Value.ApiKey;
            return request;
        }
    }
}
