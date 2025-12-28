using MediatR;

public record GetHouseRequest(int HouseId) : IRequest<GetHouseRequest.Response>
{
    public const string RouteTemplate = "/api/houses/{houseId}";

    public record Response(House? House);

    public record House(int Id, string Name, string Location, int Bedrooms, int Bathrooms,
        string? Image, int SquareFeet, string Description, int Price);
}
