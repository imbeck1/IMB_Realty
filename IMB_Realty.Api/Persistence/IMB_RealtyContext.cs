using Microsoft.EntityFrameworkCore;

public class IMB_RealtyContext : DbContext
{
    public DbSet<House> Houses => Set<House>();

    public IMB_RealtyContext(DbContextOptions<IMB_RealtyContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new HouseConfig());
    }
}