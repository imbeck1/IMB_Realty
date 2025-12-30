using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMB_Realty.Api.Persistence;

namespace IMB_Realty.Api.Features.ManageHouses.EditHouse
{
    public class GetHouseEndpoint : BaseAsyncEndpoint.WithRequest<int>.WithResponse<GetHouseRequest.Response>
    {
        private readonly IMB_RealtyContext _context;

        public GetHouseEndpoint(IMB_RealtyContext context)
        {
            _context = context;
        }

        [HttpGet(GetHouseRequest.RouteTemplate)] // Ensure this route contains {houseId}
        public override async Task<ActionResult<GetHouseRequest.Response>> HandleAsync(
            [FromRoute] int houseId, CancellationToken cancellationToken = default)
        {
            if (houseId <= 0)
            {
                // Early validation for invalid IDs
                return BadRequest("Invalid house ID.");
            }

            var house = await _context.Houses.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == houseId, cancellationToken);

            if (house == null)
            {
                return NotFound(new GetHouseRequest.Response(null));
            }

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
}

