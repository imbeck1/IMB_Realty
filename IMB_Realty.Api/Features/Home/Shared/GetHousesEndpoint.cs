using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMB_Realty.Shared.Features.Home.Shared.GetHousesRequest;

[Route("api/houses")]
public class GetHousesEndpoint 
    : BaseAsyncEndpoint.WithoutRequest
        .WithResponse<GetHousesRequest.Response>
{
    private readonly IMB_RealtyContext _context;

    public GetHousesEndpoint(IMB_RealtyContext context)
    {
        _context = context;
    }

    [HttpGet]
    public override async Task<ActionResult<GetHousesRequest.Response>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var houses = await _context.Houses.ToListAsync(cancellationToken);

        var response = new GetHousesRequest.Response(
            houses.Select(h => new GetHousesRequest.House(
                h.Id,
                h.Name,
                h.Image,
                h.Location,
                h.Price,
                h.Description,
                h.Bedrooms,
                h.Bathrooms,
                h.SquareFeet
            )).ToList()
        );

        return Ok(response);
    }
}




