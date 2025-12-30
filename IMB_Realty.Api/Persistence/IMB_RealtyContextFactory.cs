using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IMB_Realty.Api.Persistence
{
    public class IMB_RealtyContextFactory : IDesignTimeDbContextFactory<IMB_RealtyContext>
    {
        public IMB_RealtyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IMB_RealtyContext>();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data", "imbrealty.db");
            optionsBuilder.UseSqlite($"Data Source={path}");
            return new IMB_RealtyContext(optionsBuilder.Options);
        }
    }
}

