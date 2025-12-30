using System.Reflection;
using Ardalis.ApiEndpoints;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Resolve the SQLite DB path relative to the content root
var dbPath = Path.Combine(
    builder.Environment.ContentRootPath, 
    "Features", "Persistence", "Data", "Migrations", "imbrealty.db"
);

builder.Services.AddDbContext<IMB_RealtyContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Add controllers with FluentValidation
builder.Services.AddControllers().AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssembly(Assembly.Load("IMB_Realty.Shared")));

// Configure CORS for your frontend
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

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();

// Serve static files from wwwroot
app.UseStaticFiles();

// Serve Images folder if it exists
var imagesFolder = Path.Combine(builder.Environment.ContentRootPath, "Images");
if (Directory.Exists(imagesFolder))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(imagesFolder),
        RequestPath = "/Images"
    });
}

app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();


