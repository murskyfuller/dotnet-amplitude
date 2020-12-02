using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Moq;
using Dotnet.Amplitude.Enums;
using Dotnet.Amplitude.Exceptions;
using Dotnet.Amplitude.Dtos;

namespace Dotnet.Amplitude.Tests
{
    [TestClass]
    public class ClientFixture
    {
        public static readonly string SuccessResponseString = "{\"code\":200,\"events_ingested\":50,\"payload_size_bytes\":50,\"server_upload_time\":1396381378123}";
        public static Mock<HttpClient> _mockHttpClient;
        public static Mock<HttpContent> _mockHttpContent;
        public static Mock<Client> _mockAmplitudeClient;
        public static Mock<ILogger<IClient>> _mockLogger;
        public static IOptions<AmplitudeSettings> _amplitudeSettings = Options.Create(new AmplitudeSettings()
        {
            ApiKey = "test-api-key",
            BaseUrl = "http://example"
        });

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _mockHttpClient = new Mock<HttpClient>();
            _mockHttpContent = new Mock<HttpContent>();
            _mockLogger = new Mock<ILogger<IClient>>();
            _mockAmplitudeClient = new Mock<Client>(_mockHttpClient.Object, _amplitudeSettings, _mockLogger.Object) { CallBase = true };
        }

        [TestInitialize]
        public void TestInit()
        { }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockHttpClient.Reset();
            _mockHttpContent.Reset();
            _mockAmplitudeClient.Reset();
            _mockLogger.Reset();
        }

        // ******
        // UploadEvents
        // ******

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UploadEvents_ThrowsExceptionWithNullEvents()
        {
            // Act
            await _mockAmplitudeClient.Object.UploadEvents(null);
        }

        [TestMethod]
        public async Task UploadEvents_CallsPostAsJsonAsync()
        {
            // Arrange
            var events = new List<Event>
            {
                new Event { UserId = "test-user-id", DeviceId = "test-device-id", EventType = "test-event-type" }
            };

            _mockAmplitudeClient.Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(SuccessResponseString, System.Text.Encoding.UTF8, "application/json")
                });

            // Act
            await _mockAmplitudeClient.Object.UploadEvents(events);

            // Assert
            _mockAmplitudeClient.Verify(
                m => m.PostAsJsonAsync<SuccessSummaryResponse>(It.Is<string>(v => v.Equals("2/httpapi")), It.IsAny<object>()), Times.Once);
        }

        // ******
        // PostAsJsonAsync<TRes>
        // ******

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestException))]
        public async Task PostAsJsonAsync_ThrowsInvalidRequestException()
        {
            // Arrange
            var invalidRequestResponse = new InvalidRequestResponse
            {
                Code = 400,
                Error = "Request missing required field",
                MissingField = "api_key",
                EventsWithInvalidFields = new Dictionary<string, object>()
                {
                    { "time", new List<int>() { 3, 4, 7 } }
                },
                EventsWithMissingFields = new Dictionary<string, object>()
                {
                    { "event_type", new List<int>() { 3, 4, 7 } } 
                }
            };

            _mockAmplitudeClient.Setup(m => m.ReadAsAsync<IErrorResponseBody>(It.IsAny<HttpContent>(), It.IsAny<JsonSerializerOptions>()))
                .ReturnsAsync(new InvalidRequestResponse { Code = 400 });
            _mockAmplitudeClient.Setup(m => m.ReadAsAsync<InvalidRequestResponse>(It.IsAny<HttpContent>(), It.IsAny<JsonSerializerOptions>()))
                .ReturnsAsync(invalidRequestResponse);

            _mockAmplitudeClient.Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));

            // Act
            await _mockAmplitudeClient.Object.PostAsJsonAsync<SuccessSummaryResponse>("test-url", new UploadRequestBody() { });
        }

        [TestMethod]
        [ExpectedException(typeof(PayloadTooLargeException))]
        public async Task PostAsJsonAsync_ThrowsPayloadTooLargeException()
        {
            // Arrange
            var payloadTooLargeResponseBody = new PayloadTooLargeResponse
            {
                Code = 413,
                Error = "Payload too large",
            };

            _mockAmplitudeClient.Setup(m => m.ReadAsAsync<IErrorResponseBody>(It.IsAny<HttpContent>(), It.IsAny<JsonSerializerOptions>()))
                .ReturnsAsync(new InvalidRequestResponse { Code = 413 });
            _mockAmplitudeClient.Setup(m => m.ReadAsAsync<PayloadTooLargeResponse>(It.IsAny<HttpContent>(), It.IsAny<JsonSerializerOptions>()))
                .ReturnsAsync(payloadTooLargeResponseBody);

            _mockAmplitudeClient.Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.RequestEntityTooLarge));

            // Act
            await _mockAmplitudeClient.Object.PostAsJsonAsync<SuccessSummaryResponse>("test-url", new UploadRequestBody() { });
        }

        [TestMethod]
        [ExpectedException(typeof(TooManyRequestsForDeviceException))]
        public async Task PostAsJsonAsync_ThrowsTooManyRequestsForDeviceException()
        {
            // Arrange
            var tooManyRequestsResponseBody = new TooManyRequestsForDeviceResponse
            {
                Code = 429,
                Error = "Too many requests for some devices and users",
                EpsThreshold = 10,
                ThrottledDevices = new Dictionary<string, int>
                {
                    { "C8F9E604-F01A-4BD9-95C6-8E5357DF265D", 11 }
                },
                ThrottledUsers = new Dictionary<string, int>
                {
                    { "datamonster@amplitude.com", 12 }
                },
                ThrottledEvents = new List<int> { 3, 4, 7 }
            };

            _mockAmplitudeClient.Setup(m => m.ReadAsAsync<IErrorResponseBody>(It.IsAny<HttpContent>(), It.IsAny<JsonSerializerOptions>()))
                .ReturnsAsync(new InvalidRequestResponse { Code = 429 });
            _mockAmplitudeClient.Setup(m => m.ReadAsAsync<TooManyRequestsForDeviceResponse>(It.IsAny<HttpContent>(), It.IsAny<JsonSerializerOptions>()))
                .ReturnsAsync(tooManyRequestsResponseBody);

            var mockSuccessResponse = new Mock<HttpResponseMessage>();
            _mockAmplitudeClient.Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.TooManyRequests));

            // Act
            await _mockAmplitudeClient.Object.PostAsJsonAsync<SuccessSummaryResponse>("test-url", new UploadRequestBody() { });
        }

        [TestMethod]
        public async Task PostAsJsonAsync_ReturnsSuccessful()
        {
            var expectedResult = new SuccessSummaryResponse()
            {
                Code = 200,
                EventsIngested = 50,
                PayloadSizeBytes = 50,
                ServerUploadTime = 1396381378123
            };

            _mockAmplitudeClient.Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(SuccessResponseString, System.Text.Encoding.UTF8, "application/json")
                });

            // Act
            var result = await _mockAmplitudeClient.Object.PostAsJsonAsync<SuccessSummaryResponse>("test-url", new UploadRequestBody() { });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SuccessSummaryResponse));
            Assert.AreEqual(expectedResult.Code, result.Code);
            Assert.AreEqual(expectedResult.EventsIngested, result.EventsIngested);
            Assert.AreEqual(expectedResult.PayloadSizeBytes, result.PayloadSizeBytes);
            Assert.AreEqual(expectedResult.ServerUploadTime, result.ServerUploadTime);

            // calls correct url for request
            _mockAmplitudeClient.Verify(m => m.PostAsync(It.Is<string>(v => v.Equals("test-url")), It.IsAny<HttpContent>()), Times.Once);

            // does not enter error throw switch
            _mockAmplitudeClient.Verify(m => m.ReadAsAsync<IErrorResponseBody>(It.IsAny<HttpContent>(), It.IsAny<JsonSerializerOptions>()), Times.Never);

            // attempst to read a successful response
            _mockAmplitudeClient.Verify(m => m.ReadAsAsync<SuccessSummaryResponse>(It.IsAny<HttpContent>(), It.IsAny<JsonSerializerOptions>()), Times.Once);
        }
    }
}
