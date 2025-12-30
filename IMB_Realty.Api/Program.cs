using System.Reflection;
using Ardalis.ApiEndpoints;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using IMB_Realty.Api.Features.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

// ----------------------
// Configure Database
// ----------------------
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "Data", "imbrealty.db");

builder.Services.AddDbContext<IMB_RealtyContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// ----------------------
// Add Controllers + FluentValidation
// ----------------------
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssembly(Assembly.Load("IMB_Realty.Shared")));

// ----------------------
// Configure CORS for frontend
// ----------------------
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

// ----------------------
// Enable CORS BEFORE routing
// ----------------------
app.UseCors("AllowClient");

// ----------------------
// Optional: Development debugging
// ----------------------
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

// ----------------------
// HTTPS redirection + static files
// ----------------------
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();

// Serve static files
app.UseStaticFiles(); 
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "Images")
    ),
    RequestPath = "/Images"
});

app.UseRouting();

// ----------------------
// Map controllers + fallback
// ----------------------
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();




