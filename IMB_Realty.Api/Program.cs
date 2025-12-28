using System.Reflection;
using Ardalis.ApiEndpoints;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<IMB_RealtyContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("IMB_RealtyContext")));

builder.Services.AddControllers().AddFluentValidation(
    fv => fv.RegisterValidatorsFromAssembly(Assembly.Load("IMB_Realty.Shared")));

// Configure CORS: allow both local dev and deployed frontend
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins(
                "https://lively-water-0aff8551e.2.azurestaticapps.net", // deployed frontend
                "https://localhost:5001") // local frontend
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Middleware pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseBlazorFrameworkFiles();

app.UseRouting();

// Apply CORS
app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

