using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IMB_Realty.Api.Persistence
{
    public class IMB_RealtyContextFactory : IDesignTimeDbContextFactory<IMB_RealtyContext>
    {
        public IMB_RealtyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IMB_RealtyContext>();
            optionsBuilder.UseSqlServer("Server=tcp:imbrealtyserver.database.windows.net,1433;Initial Catalog=IMB_RealtyDB;Persist Security Info=False;User ID=dorko101;Password=ExpelledKitties32?;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return new IMB_RealtyContext(optionsBuilder.Options);
        }
    }
}


