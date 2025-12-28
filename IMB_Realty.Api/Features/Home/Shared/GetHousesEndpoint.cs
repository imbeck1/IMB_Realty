using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ardalis.ApiEndpoints;
using IMB_Realty.Shared.Features.Home.Shared.GetHousesRequest;

[ApiController]
public class GetHousesEndpoint : BaseAsyncEndpoint.WithoutRequest.WithResponse<GetHousesRequest.Response>
{
    private readonly IMB_RealtyContext _context;

    public GetHousesEndpoint(IMB_RealtyContext context)
    {
        _context = context;
    }

    [HttpGet("api/houses")]  // <-- MUST exactly match the client call
    public override async Task<ActionResult<GetHousesRequest.Response>> HandleAsync(CancellationToken cancellationToken = default)
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



