using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TacoMusings.UI;
using TacoMusings.UI.Services;
using TacoMusings.UI.Services.Interfaces;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<ITacoDataService, TacoDataService>(client => client.BaseAddress = new Uri("http://www.arstechnica.com"));

// builder.Services.AddScoped<ITacoDataService, TacoDataService>();

await builder.Build().RunAsync();
