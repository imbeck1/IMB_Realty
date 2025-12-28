using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ardalis.ApiEndpoints;
using IMB_Realty.Shared.Features.Home.Shared.GetHousesRequest;


public class GetHousesEndpoint : BaseAsyncEndpoint.WithRequest<int>.WithResponse<GetHousesRequest.Response>
{
    private readonly IMB_RealtyContext _context;

    public GetHousesEndpoint(IMB_RealtyContext context)
    {
        _context = context;
    }

    [HttpGet(GetHousesRequest.RouteTemplate)]
    public override async Task<ActionResult<GetHousesRequest.Response>> HandleAsync(int houseId, CancellationToken cancellationToken = default)
    {
        var houses = await _context.Houses.ToListAsync(cancellationToken);

        var response = new GetHousesRequest.Response(houses.Select(house => new GetHousesRequest.House(
            house.Id,
            house.Name,
            house.Image,
            house.Location,
            house.Price,
            house.Description,
            house.Bedrooms,
            house.Bathrooms,
            house.SquareFeet
        )));

        return Ok(response);
    }
}