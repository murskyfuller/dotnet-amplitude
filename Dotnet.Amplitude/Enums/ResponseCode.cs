using System.Net;

namespace Dotnet.Amplitude.Enums
{
    public enum ResponseCode
    {
        Success = HttpStatusCode.OK,
        InvalidRequest = HttpStatusCode.BadRequest,
        PayloadTooLarge = HttpStatusCode.RequestEntityTooLarge,
        TooManyRequestsForDevice = HttpStatusCode.TooManyRequests
    }
}
