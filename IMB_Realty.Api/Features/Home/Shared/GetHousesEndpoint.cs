using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMB_Realty.Shared.Features.Home.Shared.GetHousesRequest;
using IMB_Realty.Api.Persistence;

namespace IMB_Realty.Api.Features.Home.Shared
{
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
            var houses = await _context.Houses
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var response = new GetHousesRequest.Response(
                houses.Select(h => new GetHousesRequest.House(
                    h.Id,
                    h.Name ?? string.Empty,
                    h.Image != null ? Convert.ToBase64String(h.Image) : null, // Convert byte[] to Base64
                    h.Location ?? string.Empty,
                    h.Price,
                    h.Description ?? string.Empty,
                    h.Bedrooms,
                    h.Bathrooms,
                    h.SquareFeet
                )).ToList()
            );

            return Ok(response);
        }
    }
}






