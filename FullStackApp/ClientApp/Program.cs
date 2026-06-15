// InventoryHub - Blazor WASM Front-End bootstrap (ClientApp)
// Activity 1: Copilot recommended scoping HttpClient with an explicit BaseAddress
//             pointing to the ServerApp origin (localhost:5112) so every relative
//             URL in the app resolves to the correct API host automatically.
// Activity 2: Copilot added the 10-second Timeout to surface slow/hung requests
//             as a TaskCanceledException rather than waiting indefinitely.

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5112"),
    Timeout = TimeSpan.FromSeconds(10)
});

await builder.Build().RunAsync();
