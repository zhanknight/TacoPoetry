using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TacoMusings.UI;
using TacoMusings.UI.Services;
using TacoMusings.UI.Services.Interfaces;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<ITacoDataService, TacoDataService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5221/");
    client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
    client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    client.DefaultRequestHeaders.Add("Access-Control-Request-Headers", "content-type");
    client.DefaultRequestHeaders.Add("accept", "application/json");

})
    ;

// builder.Services.AddScoped<ITacoDataService, TacoDataService>();

await builder.Build().RunAsync();
