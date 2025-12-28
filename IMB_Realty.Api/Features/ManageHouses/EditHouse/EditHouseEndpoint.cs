using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMB_Realty.Api.Persistence;
using IMB_Realty.Shared.Features.ManageHouses;

public class EditHouseEndpoint : BaseAsyncEndpoint
    .WithRequest<EditHouseRequest>
    .WithResponse<bool>
{
    private readonly IMB_RealtyContext _context;

    public EditHouseEndpoint(IMB_RealtyContext context) => _context = context;

    [HttpPut(EditHouseRequest.RouteTemplate)]
    public override async Task<ActionResult<bool>> HandleAsync(
        EditHouseRequest request,
        CancellationToken cancellationToken = default)
    {
        var house = await _context.Houses.SingleOrDefaultAsync(
            x => x.Id == request.House.Id,
            cancellationToken);

        if (house is null)
            return BadRequest("House not found.");

        // Update house fields
        house.Name = request.House.Name;
        house.Description = request.House.Description;
        house.Location = request.House.Location;
        house.Price = request.House.Price;
        house.Bedrooms = request.House.Bedrooms;
        house.Bathrooms = request.House.Bathrooms;
        house.SquareFeet = request.House.SquareFeet;

        // Handle image actions
        if (request.House.ImageAction == ImageAction.Remove && !string.IsNullOrWhiteSpace(house.Image))
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", house.Image);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            house.Image = null;
        }
        else if (request.House.ImageAction == ImageAction.Add && !string.IsNullOrWhiteSpace(request.House.Image))
        {
            // Save new image name (already uploaded by UploadHouseImageRequest)
            house.Image = request.House.Image;
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Ok(true);
    }
}

