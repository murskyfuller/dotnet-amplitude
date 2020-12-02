namespace Dotnet.Amplitude.Dtos
{
    public interface IDeviceMetadata
    {
        /// <summary>
        /// The current version of your application.
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        /// Platform of the device.
        /// </summary>
        string Platform { get; set; }

        /// <summary>
        /// The name of the mobile operating system or browser that the user is using.
        /// </summary>
        string OsName { get; set; }

        /// <summary>
        /// The version of the mobile operating system or browser the user is using.
        /// </summary>
        string OsVersion { get; set; }

        /// <summary>
        /// The device brand that the user is using.
        /// </summary>
        string DeviceBrand { get; set; }

        /// <summary>
        /// The device manufacturer that the user is using.
        /// </summary>
        string DeviceManufacturer { get; set; }

        /// <summary>
        /// The device model that the user is using.
        /// </summary>
        string DeviceModel { get; set; }

        /// <summary>
        /// The carrier that the user is using.
        /// </summary>
        string Carrier { get; set; }
    }
}
