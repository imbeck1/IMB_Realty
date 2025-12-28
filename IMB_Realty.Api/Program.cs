using System.Reflection;
using Ardalis.ApiEndpoints;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<IMB_RealtyContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("IMB_RealtyContext")));

builder.Services.AddControllers().AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssembly(Assembly.Load("IMB_Realty.Shared")));

// Configure CORS for your deployed frontend
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

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

