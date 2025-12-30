using System.Reflection;
using Ardalis.ApiEndpoints;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using IMB_Realty.Api.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services

// ----------------------------
// Use Azure SQL instead of SQLite
// ----------------------------
builder.Services.AddDbContext<IMB_RealtyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers and FluentValidation
builder.Services.AddControllers().AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssembly(Assembly.Load("IMB_Realty.Shared")));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy
            .WithOrigins("https://lively-water-0aff8551e.2.azurestaticapps.net")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable CORS before routing
app.UseCors("AllowClient");

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

// HTTPS redirection and Blazor static files
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

// Serve wwwroot/Data folder
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data")),
    RequestPath = "/Data"
});

app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();






