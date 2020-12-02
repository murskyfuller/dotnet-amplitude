using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dotnet.Amplitude.Dtos;

namespace Dotnet.Amplitude.Tests
{
    [TestClass]
    public class NamingPoliciesFixture
    {
        public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            IgnoreNullValues = true,
            PropertyNamingPolicy = NamingPolicies.SnakeCase
        };

        [TestMethod]
        public void SnakeCaseNamingPolicy_CorrectlySerializes()
        {
            // Arrange
            var expectedSerializedResult = "{\"api_key\":\"test-api-key\",\"events\":[{\"user_id\":\"test-user-id\",\"device_id\":\"test-device-id\",\"event_type\":\"test-event-type\"}]}";
            var body = new UploadRequestBody
            {
                ApiKey = "test-api-key",
                Events = new List<Event> { new Event { UserId = "test-user-id", DeviceId = "test-device-id", EventType = "test-event-type"  } }
            };

            // Act
            var serialized = JsonSerializer.Serialize(body, SerializerOptions);

            // Assert
            Assert.AreEqual(expectedSerializedResult, serialized);
        }

        [TestMethod]
        public void SnakeCaseNamingPolicy_CorrectlyDeserializes()
        {
            // Arrange
            var jsonString = "{\"api_key\": \"test-api-key\", \"events\": [ { \"user_id\": \"test-user-id\", \"device_id\": \"test-device-id\", \"event_type\": \"test-event-type\" } ]}";
            var expectedEvent = new Event { UserId = "test-user-id", DeviceId = "test-device-id", EventType = "test-event-type" };
            var expectedResult = new UploadRequestBody
            {
                ApiKey = "test-api-key",
                Events = new List<Event> { expectedEvent }
            };

            // Act
            var deserialized = JsonSerializer.Deserialize<UploadRequestBody>(jsonString, SerializerOptions);

            // Assert
            Assert.AreEqual(expectedResult.ApiKey, deserialized.ApiKey);
            Assert.AreEqual(1, deserialized.Events.Count());

            var ev = deserialized.Events.First();
            Assert.AreEqual(expectedEvent.UserId, ev.UserId);
            Assert.AreEqual(expectedEvent.DeviceId, ev.DeviceId);
            Assert.AreEqual(expectedEvent.EventType, ev.EventType);
        }
    }
}
