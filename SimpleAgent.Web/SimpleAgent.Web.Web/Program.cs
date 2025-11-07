using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SimpleAgent.Web.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddBootstrapBlazor();

builder.Services.AddHttpClient<SimplAgentApiClient>(client =>
{
    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
    client.BaseAddress = new("https+http://apiservice");
});


await builder.Build().RunAsync();


