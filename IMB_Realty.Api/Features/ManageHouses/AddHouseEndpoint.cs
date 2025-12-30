using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using IMB_Realty.Api.Persistence;
using IMB_Realty.Api.Persistence.Data.Entities;
using System.IO;

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
        byte[]? imageBytes = null;

        // Convert uploaded image to byte array
        if (request.Image != null && request.Image.Length > 0)
        {
            using var memoryStream = new MemoryStream();
            await request.Image.CopyToAsync(memoryStream, cancellationToken);
            imageBytes = memoryStream.ToArray();
        }

        var house = new House
        {
            Name = request.House.Name,
            Description = request.House.Description,
            Location = request.House.Location,
            Bedrooms = request.House.Bedrooms,
            Bathrooms = request.House.Bathrooms,
            SquareFeet = request.House.SquareFeet,
            Price = request.House.Price,
            Image = imageBytes // Store image as byte[]
        };

        await _database.Houses.AddAsync(house, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);

        return Ok(house.Id);
    }
}


