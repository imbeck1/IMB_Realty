using System.Reflection;
using Ardalis.ApiEndpoints;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services
// SQLite DB path relative to the app's current directory
var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data", "imbrealty.db");

builder.Services.AddDbContext<IMB_RealtyContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Add controllers + FluentValidation
builder.Services.AddControllers().AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssembly(Assembly.Load("IMB_Realty.Shared")));

// Configure CORS for the deployed frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy
            .WithOrigins("https://lively-water-0aff8551e.2.azurestaticapps.net") // frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable CORS before routing
app.UseCors("AllowClient");

app.UseHttpsRedirection();
app.UseStaticFiles(); // this will serve wwwroot automatically
app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();



