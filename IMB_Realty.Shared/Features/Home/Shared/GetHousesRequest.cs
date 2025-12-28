namespace IMB_Realty.Shared.Features.Home.Shared.GetHousesRequest;

using MediatR;

public record GetHousesRequest : IRequest<GetHousesRequest.Response>
{
    public const string RouteTemplate = "/api/houses";

    public record House(int Id, string Name, string? Image, string Location, int Price, string Description, int Bedrooms, int Bathrooms, int SquareFeet);
    public record Response(IEnumerable<House> Houses);
}