namespace Dotnet.Amplitude.Dtos
{
    public interface IPurchaseMetadata
    {
        /// <summary>
        /// The price of the item purchased. Required for revenue data if the revenue field is not sent. You can use negative values to indicate refunds.
        /// </summary>
        float? Price { get; set; }

        /// <summary>
        /// The quantity of the item purchased. Defaults to 1 if not specified.
        /// </summary>
        int? Quantity { get; set; }

        /// <summary>
        /// revneue = price quantity. If you send all 3 fields of price, quantity, and revenue, then (price quantity) will be used as the revenue value. You can use negative values to indicate refunds.
        /// </summary>
        float? Revenue { get; set; }

        /// <summary>
        /// An identifier for the item purchased. You must send a price and quantity or revenue with this field.
        /// </summary>
        string ProductId { get; set; }

        /// <summary>
        /// The type of revenue for the item purchased. You must send a price and quantity or revenue with this field.
        /// </summary>
        string RevenueType { get; set; }
    }
}
