namespace Dotnet.Amplitude.Dtos
{
    public interface ILocationMetadata
    {
        /// <summary>
        /// The current country of the user.
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// The current region of the user.
        /// </summary>
        string Region { get; set; }

        /// <summary>
        /// The current city of the user.
        /// </summary>
        string City { get; set; }

        /// <summary>
        /// The current Designated Market Area of the user.
        /// </summary>
        string Dma { get; set; }

        /// <summary>
        /// The current Latitude of the user.
        /// </summary>
        float? LocationLat { get; set; }

        /// <summary>
        /// The current Longitude of the user.
        /// </summary>
        float? LocationLong { get; set; }

        /// <summary>
        /// The language set by the user.
        /// </summary>
        string Language { get; set; }
    }
}
