using Microsoft.EntityFrameworkCore;

namespace IMB_Realty.Api.Features.Persistence
{
    public class IMB_RealtyContext : DbContext
    {
        public IMB_RealtyContext(DbContextOptions<IMB_RealtyContext> options)
            : base(options)
        {
        }

        public DbSet<House> Houses { get; set; }
    }

    public class House
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Image { get; set; }
        public string Location { get; set; } = default!;
        public int Price { get; set; }
        public string Description { get; set; } = default!;
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int SquareFeet { get; set; }
    }
}
