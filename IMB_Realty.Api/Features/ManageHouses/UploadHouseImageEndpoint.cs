using System.Drawing;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using IMB_Realty.Api.Persistence;


public class UploadHouseImageEndpoint : BaseAsyncEndpoint.WithRequest<int>.WithResponse<string>
{
    private readonly IMB_RealtyContext _database;

    public UploadHouseImageEndpoint(IMB_RealtyContext database)
    {
        _database = database;
    }

    [HttpPost(UploadHouseImageRequest.RouteTemplate)]
    public override async Task<ActionResult<string>> HandleAsync([FromRoute] int houseId, CancellationToken cancellationToken = default)
    {
        var house = await _database.Houses.SingleOrDefaultAsync(x => x.Id == houseId, cancellationToken);

        if (house is null)
        {
            return BadRequest("House does not exist");
        }

        var file = Request.Form.Files[0];
        if (file.Length == 0)
        {
            return BadRequest("No Image found");
        }

        var filename = $"{Guid.NewGuid()}.jpg";
        var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "Images", filename);

        var resizeOptions = new ResizeOptions
        {
            Mode = ResizeMode.Pad,
            Size = new SixLabors.ImageSharp.Size(640, 426)
        };

        using var image = Image.Load(file.OpenReadStream());
        image.Mutate(x => x.Resize(resizeOptions));
        await image.SaveAsJpegAsync(saveLocation, cancellationToken: cancellationToken);

        if(!string.IsNullOrWhiteSpace(house.Image))
        {
            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", house.Image));
        }

        house.Image = filename;
        await _database.SaveChangesAsync(cancellationToken);

        return Ok(house.Image);
    }
}
