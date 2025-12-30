using MediatR;

namespace IMB_Realty.Shared.Features.Home.Shared.GetHousesRequest;

public record GetHousesRequest : IRequest<GetHousesRequest.Response>
{
    // IMPORTANT: no leading slash
    public const string RouteTemplate = "api/houses";

    public record House(
        int Id,
        string Name,
        string? Image,
        string Location,
        int Price,
        string Description,
        int Bedrooms,
        int Bathrooms,
        int SquareFeet
    );

    public record Response(IEnumerable<House> Houses);
}
