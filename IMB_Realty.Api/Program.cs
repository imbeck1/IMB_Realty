using System.Reflection;
using Ardalis.ApiEndpoints;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------

builder.Services.AddDbContext<IMB_RealtyContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("IMB_RealtyContext")));

builder.Services
    .AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssembly(Assembly.Load("IMB_Realty.Shared")));

// CORS – allow deployed Blazor frontend
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

// --------------------
// Middleware
// --------------------

// MUST be early
app.UseCors("AllowClient");

app.UseHttpsRedirection();

// --------------------
// Static Files (Images)
// --------------------

// Use ContentRootPath (Azure-safe)
var imagesPath = Path.Combine(app.Environment.ContentRootPath, "Images");

// ✅ Prevent startup crash
if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}

app.UseStaticFiles(); // wwwroot (if any)

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "/Images"
});

app.UseRouting();

// --------------------
// Endpoints
// --------------------

app.MapControllers();

// ❌ DO NOT use Blazor middleware in API
// app.UseBlazorFrameworkFiles();
// app.MapFallbackToFile("index.html");

app.Run();

