namespace Dotnet.Amplitude.Dtos
{
    public interface IErrorResponseBody : IResponseBody
    {
        string Error { get; set; }
    }
}
