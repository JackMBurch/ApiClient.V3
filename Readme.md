<div align="center">
    <h1>C# DigiKey API Client Library</h1>

![image](https://i.imgur.com/gLgV1tB.png)

</div>



### Features

* Makes structured calls to the DigiKey API from .NET projects
* Handles the OAuth2 control flow, logs users in, refreshes tokens when needed, etc.

### Basic Usage

```csharp
var settings = ApiClientSettings.CreateFromConfigFile();
var client = new ApiClientService(settings);

var response = await client.ProductInformation.KeywordSearch("P5555-ND");

var jsonFormatted = JToken.Parse(response).ToString(Newtonsoft.Json.Formatting.Indented);
Console.WriteLine($"Reponse is {jsonFormatted}");
```

### Project Contents

* **ApiClient** - Core client Library that manages a users OAuth2 config file, settings, and authorization flow. It also contains methods for endpoints belonging to the [Product Information API](https://developer.digikey.com/products/product-information).

* **ApiClient.ConsoleApp** - Console app to test the refresh of access tokens using a sample KeywordSearch.
* **OAuth2Service.ConsoleApp** - Console app to do the initial authorization and get a new access and refresh token.

### Getting Started  

1. Clone the repository or download and extract the zip file containing the ApiClient solution.
2. You will need to register an application on the [DigiKey Developer Portal](https://developer.digikey.com/) in order to create your unique Client ID, Client Secret as well as to set your redirection URI.
3. In the solution folder copy apiclientexample.config as apiclient.config, and update it with the ClientId, ClientSecret, and RedirectUri values from step 2.
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <appSettings>
        <add key="ApiClient.ClientId"" value="YOUR_CLIENT_ID_HERE" />
        <add key="ApiClient.ClientSecret" value="YOUR_CLIENT_SECRET_HERE" />
        <add key="ApiClient.RedirectUri"  value="YOUR_REDIRECT_URI_HERE" />
        <add key="ApiClient.AccessToken" value="" />
        <add key="ApiClient.RefreshToken" value="" />
        <add key="ApiClient.ExpirationDateTime" value="" />
    </appSettings>
</configuration>
```
4. Set the DigikeyProduction environment variable to either true or false for each project you plan to use the ApiClient library in. You can do this by right clicking on the project in Visual Studio and selecting Properties. Then select the Debug tab and add the environment variable in the Environment Variables section.
5. Run OAuth2Service.ConsoleApp to set the access token, refresh token and expiration date in apiclient.config. 
6. Run ApiClient.ConsoleApp to get results from keyword search.
