namespace Dotnet.Amplitude
{
    public class AmplitudeSettings
    {
        public string BaseUrl { get; set; } = "https://api.amplitude.com";
        public string ApiKey { get; set; }
        public string IdHash { get; set; }
    }
}
