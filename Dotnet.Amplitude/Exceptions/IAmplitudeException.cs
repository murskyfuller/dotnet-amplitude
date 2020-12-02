using Dotnet.Amplitude.Enums;

namespace Dotnet.Amplitude.Exceptions
{
    public interface IAmplitudeException
    {
        ResponseCode Code { get; set; }
        string ErrorMessage { get; set; }
    }
}
