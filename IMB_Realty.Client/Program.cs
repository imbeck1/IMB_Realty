using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IMB_Realty.Client;
using MediatR;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://imb-realty-api.azurewebsites.net/")
    });


await builder.Build().RunAsync();

