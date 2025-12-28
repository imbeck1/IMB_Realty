using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMB_Realty.Api.Persistence;

public class GetHouseEndpoint : BaseAsyncEndpoint.WithRequest<int>.WithResponse<GetHouseRequest.Response>
{
    private readonly IMB_RealtyContext _context;

    public GetHouseEndpoint(IMB_RealtyContext context) => _context = context;

    [HttpGet(GetHouseRequest.RouteTemplate)]
    public override async Task<ActionResult<GetHouseRequest.Response>> HandleAsync(int houseId, CancellationToken cancellationToken = default)
    {
        var house = await _context.Houses.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == houseId, cancellationToken);

        if (house is null)
            return NotFound(new GetHouseRequest.Response(null));

        var response = new GetHouseRequest.Response(new GetHouseRequest.House(
            house.Id,
            house.Name,
            house.Location,
            house.Bedrooms,
            house.Bathrooms,
            house.Image,
            house.SquareFeet,
            house.Description,
            house.Price
        ));

        return Ok(response);
    }
}
