namespace Dotnet.Amplitude.Dtos
{
    public interface IDeviceIdentifierMetadata
    {
        /// <summary>
        /// A device-specific identifier, such as the Identifier for Vendor on iOS. Required unless user_id is present. If a device_id is not sent with the event, it will be set to a hashed version of the user_id.
        /// </summary>
        string DeviceId { get; set; }

        /// <summary>
        /// The IP address of the user. Use "$remote" to use the IP address on the upload request. We will use the IP address to reverse lookup a user's location (city, country, region, and DMA). Amplitude has the ability to drop the location and IP address from events once it reaches our servers. You can submit a request to our platform specialist team here to configure this for you.
        /// </summary>
        string Ip { get; set; }

        /// <summary>
        /// (iOS) Identifier for Advertiser.
        /// </summary>
        string Idfa { get; set; }

        /// <summary>
        /// (iOS) Identifier for Vendor.
        /// </summary>
        string Idfv { get; set; }

        /// <summary>
        /// (Android) Google Play Services advertising ID
        /// </summary>
        string Adid { get; set; }

        /// <summary>
        /// (Android) Android ID (not the advertising ID)
        /// </summary>
        string AndroidId { get; set; }
    }
}
