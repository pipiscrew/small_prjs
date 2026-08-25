# OAuth2 sample for framework v4.6.2

This sample does not use any third-party assemblies.

📌 [article](https://www.pipiscrew.com/threads/154455/)  

<img width="286" height="171" alt="Image" src="https://github.com/user-attachments/assets/07ead061-9d07-459f-9c10-a98ede48d537" />  

<img width="648" height="40" alt="Image" src="https://github.com/user-attachments/assets/ebc8fe1a-d558-4b57-8b15-6ec1cc195085" />  

<img width="353" height="172" alt="Image" src="https://github.com/user-attachments/assets/6c8915a3-3cd7-4003-8a43-bf7dd35af45c" />  

references :
* <https://stackoverflow.com/a/11457719>  
* <https://stackoverflow.com/a/64875381>  
* [Bee.OAuth2.WinForms](https://www.nuget.org/packages/Bee.OAuth2.WinForms) - OAuth2 + use of webview2
* [RestSharp](https://www.nuget.org/packages/RestSharp/)
* [DotNetCoreOauth2](https://github.com/alkampfergit/DotNetCoreOauth2) [[presentation](https://www.youtube.com/watch?v=hOgvTDKKgnY)]  

> **OAuth 2.0**: the authorization framework that obtains and scopes access tokens.  
> **XOAUTH2**: an email-specific SASL authentication method that presents an OAuth access token to an IMAP, POP3, or SMTP server.  

* [OAuth2 SMTP and Microsoft Graph](https://services.dartmouth.edu/TDClient/1806/Portal/KB/Article/168187/Sending-Email-SMTP-OAuth2-FAQ-for-Application-Teams-Developers)

---

Switch from HttpListener to **Kestrel** (the modern web server built into .NET Core / .NET 6/8/9), you completely bypass **HTTP.sys** and **netsh**.

## .NET variant

By default, a WinForms app uses the Microsoft.NET.Sdk.WindowsDesktop or Microsoft.NET.Sdk SDK. To use Kestrel and WebApplication, you need to tell it to also include the Web SDK components.

* Double-click your WinForms project name in Visual Studio to open the .csproj file.
* Change the very first line (`Sdk="..."`) to `Microsoft.NET.Sdk.Web`.  
Your project file should look clean like this:  

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework> <!-- or net6.0 / net9.0 -->
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

*Don't worry: Adding the Web SDK will not break your WinForms UI; it just unlocks the web libraries like Kestrel*  

Now you can boot up Kestrel asynchronously right when your application launches. Here is a clean, production-ready example of how to structure your Program.cs file so the web server starts up gracefully alongside your UI:  

```csharp
namespace MyWinFormsApp
{
    internal static class Program
    {
        [STAThread]
        static async Task Main(string[] args)
        {
            // 1. Standard WinForms initialization
            ApplicationConfiguration.Initialize();

            // 2. Configure and start the Kestrel background web server
            var webBuilder = WebApplication.CreateBuilder(args);

            // Configure Kestrel to use your self-contained SSL certificate file
            webBuilder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenLocalhost(49152, listenOptions =>
                {
                    // Loads a .pfx file right from your app folder! No installation needed!
                    listenOptions.UseHttps("dev_cert.pfx", "YourCertPassword");
                });
            });

            var webApp = webBuilder.Build();

            // Setup your callback API endpoint
            webApp.MapGet("/oauth-callback", (string token) =>
            {
                // Handle your incoming token here!
                // You can pass it to your open form using an event or a static property.
                return Results.Ok("Authentication successful! You can close this tab.");
            });

            // Start Kestrel in the background (Non-blocking)
            await webApp.StartAsync();

            // 3. Launch your WinForms UI
            Application.Run(new Form1());

            // 4. Clean up the web server when the user closes the WinForms app
            await webApp.StopAsync();
        }
    }
}

```