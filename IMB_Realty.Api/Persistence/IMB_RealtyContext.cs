using Microsoft.EntityFrameworkCore;
using IMB_Realty.Api.Persistence.Data.Entities;

namespace IMB_Realty.Api.Persistence
{
    public class IMB_RealtyContext : DbContext
    {
        public IMB_RealtyContext(DbContextOptions<IMB_RealtyContext> options)
            : base(options) { }

        public DbSet<House> Houses { get; set; }
    }
}
