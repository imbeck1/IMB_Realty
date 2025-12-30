using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using IMB_Realty.Api.Persistence;


namespace IMB_Realty.Api.Features.ManageHouses;

public class AddHouseEndpoint : BaseAsyncEndpoint.WithRequest<AddHouseRequest>.WithResponse<int>
{
    private readonly IMB_RealtyContext _database;

    public AddHouseEndpoint(IMB_RealtyContext database)
    {
        _database = database;
    }

    [HttpPost(AddHouseRequest.RouteTemplate)]
    public override async Task<ActionResult<int>> HandleAsync(AddHouseRequest request, CancellationToken cancellationToken = default)
    {
        var house = new House
        {
            Name = request.House.Name,
            Description = request.House.Description,
            Location = request.House.Location,
            Bedrooms = request.House.Bedrooms,
            Bathrooms = request.House.Bathrooms,
            SquareFeet = request.House.SquareFeet,
            Price = request.House.Price
        };

        await _database.Houses.AddAsync(house, cancellationToken);

        await _database.SaveChangesAsync(cancellationToken);

        return Ok(house.Id);
    }
}
