using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ardalis.ApiEndpoints;
using IMB_Realty.Shared.Features.Home.Shared.GetHousesRequest;

namespace IMB_Realty.Api.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetHousesEndpoint : BaseAsyncEndpoint
        .WithoutRequest
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
            // Fetch all houses
            var houses = await _context.Houses.ToListAsync(cancellationToken);

            // Map to response DTOs
            var response = new GetHousesRequest.Response(
                houses.Select(house => new GetHousesRequest.House(
                    house.Id,
                    house.Name ?? string.Empty,
                    house.Image ?? string.Empty,
                    house.Location ?? string.Empty,
                    house.Price,
                    house.Description ?? string.Empty,
                    house.Bedrooms,
                    house.Bathrooms,
                    house.SquareFeet
                ))
            );

            return Ok(response);
        }
    }
}
