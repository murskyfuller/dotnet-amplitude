Make requests to Amplitude API endpoints, currently only for posting events.

This package was created as no such formal package currently exists for .NET Core from Amplitude.

# **Usage**
For direct injection, first add your ApiKey appsettings.json. You can also set "BaseUrl" here, but is not necessary as it defaults to "https://api.amplitude.com".
```
{
    "ApiKey": "your-api-key"
}
```

In your Startup, configure the appsettings.json section for direct injection as IOptions<AmplitudeSettings>.
```
    public void ConfigureServices(IServiceCollection services)
    {
        ...
        services.Configure<AmplitudeSettings>(Configuration.GetSection("Amplitude"));
        ...
    }
```

Then register the client for direct injection. It will use Direct Injection to automatically add the ApiKey to each request.
```
    public void ConfigureServices(IServiceCollection services)
    {
        ...
        services.AddHttpClient<IClient, Client>();
        ...
    }
```

If you'd like you have the option to set the BaseAddress during registration. The value set during injection takes precedent over one in appsettings.json
```
    public void ConfigureServices(IServiceCollection services)
    {
        ...
        services.AddHttpClient<IClient, Client>(httpClient => httpClient.BaseUrl = new Uri("http://some-other-base-url"));
        ...
    }
```

# **API**
## **Client**
###### **HttpClient HttpClient** { get; }
###### **Task<SuccessSummaryResponse> UploadEvents(ICollection<Event> events);**
###### **Task<TRes> PostAsJsonAsync<TRes>(string path, object body = null);**
###### **Task<TRes> ReadAsAsync<TRes>(HttpContent content, JsonSerializerOptions serializerOptions);**
###### **Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);**

## **Event**
###### **string DeviceId** { get; set; } [Required]
###### **string EventType** { get; set; } [Required]
###### **long? Time** { get; set; } [Required]
###### **IDictionary<string, object> EventProperties** { get; set; }
###### **IDictionary<string, object> UserProperties** { get; set; }
###### **IDictionary<string, object> Groups** { get; set; }
###### **string AppVersion** { get; set; }
###### **string Platform** { get; set; }
###### **string OsName** { get; set; }
###### **string OsVersion** { get; set; }
###### **string DeviceBrand** { get; set; }
###### **string DeviceManufacturer** { get; set; }
###### **string DeviceModel** { get; set; }
###### **string Carrier** { get; set; }
###### **string Country** { get; set; }
###### **string Region** { get; set; }
###### **string City** { get; set; }
###### **string Dma** { get; set; }
###### **string Language** { get; set; }
###### **float? Price** { get; set; }
###### **int? Quantity** { get; set; }
###### **float? Revenue** { get; set; }
###### **string ProductId** { get; set; }
###### **string RevenueType** { get; set; }
###### **float? LocationLat** { get; set; }
###### **float? LocationLong** { get; set; }
###### **string Ip** { get; set; }
###### **string Idfa** { get; set; }
###### **string Idfv** { get; set; }
###### **string Adid** { get; set; }
###### **string AndroidId** { get; set; }
###### **int? EventId** { get; set; }
###### **long? SessionId** { get; set; }
###### **string InsertId** { get; set; }