using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class House
{
    public int Id
    {
        get;
        set;
    }

    public string Name
    {
        get;
        set;
    } = "";

    public int Price
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    } = "";

    public string? Image
    {
        get;
        set;
    } = "";

    public string Location
    {
        get;
        set;
    } = "";

    public int Bedrooms
    {
        get;
        set;
    }

    public int Bathrooms
    {
        get;
        set;
    }

    public int SquareFeet
    {
        get;
        set;
    }
}

public class HouseConfig : IEntityTypeConfiguration<House>
{
    public void Configure(EntityTypeBuilder<House> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Location).IsRequired();
        builder.Property(x => x.Bedrooms).IsRequired();
        builder.Property(x => x.Bathrooms).IsRequired();
        builder.Property(x => x.SquareFeet).IsRequired();
        builder.Property(x => x.Price).IsRequired();
    }
}