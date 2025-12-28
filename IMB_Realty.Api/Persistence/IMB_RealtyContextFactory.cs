using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class IMB_RealtyContextFactory : IDesignTimeDbContextFactory<IMB_RealtyContext>
{
    public IMB_RealtyContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../IMB_Realty.Api");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<IMB_RealtyContext>();
        var connectionString = configuration.GetConnectionString("IMB_RealtyContext");

        optionsBuilder.UseSqlite(connectionString);

        return new IMB_RealtyContext(optionsBuilder.Options);
    }
}

