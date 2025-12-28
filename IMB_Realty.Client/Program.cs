using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IMB_Realty.Client;
using MediatR;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Single HttpClient pointing to local API in dev, deployed API in production
var apiBaseUrl = builder.HostEnvironment.IsDevelopment()
    ? "https://localhost:5001/" // local API URL
    : "https://imb-realty-api-hjh2a2d8heangac3.canadacentral-01.azurewebsites.net/"; // deployed API URL

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

// MediatR
builder.Services.AddMediatR(typeof(Program).Assembly);

await builder.Build().RunAsync();



