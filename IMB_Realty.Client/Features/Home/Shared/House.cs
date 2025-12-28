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

    public string Image
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

    public IEnumerable<RouteInstruction> Route
    {
        get;
        set;
    } = Array.Empty<RouteInstruction>();
}

public class RouteInstruction
{
    public int Stage
    {
        get;
        set;
    }
    public string Description
    {
        get;
        set;
    } = "";
}