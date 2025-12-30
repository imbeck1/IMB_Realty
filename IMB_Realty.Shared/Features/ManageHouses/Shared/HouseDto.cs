using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace IMB_Realty.Shared.Features.ManageHouses;

public class HouseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public int Price { get; set; }

    public string Location { get; set; } = "";

    public int Bedrooms { get; set; }

    public int Bathrooms { get; set; }

    public int SquareFeet { get; set; }

    // Image file uploaded by the user
    public IFormFile? ImageFile { get; set; }

    // Optional base64 representation for displaying existing images
    public string? ImageBase64 { get; set; }

    public ImageAction ImageAction { get; set; } = ImageAction.None;
}

public enum ImageAction
{
    None,
    Add,
    Remove
}

public class HouseValidator : AbstractValidator<HouseDto>
{
    public HouseValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter a Name");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a Description");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Please enter a Location");
        RuleFor(x => x.Bedrooms).NotEmpty().WithMessage("Please enter Bedroom number");
        RuleFor(x => x.Bathrooms).NotEmpty().WithMessage("Please enter Bathroom number");
        RuleFor(x => x.SquareFeet).NotEmpty().WithMessage("Please enter Square Footage");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Please enter a Price");
        
        // Optional: validate uploaded image file size or type
        RuleFor(x => x.ImageFile)
            .Must(f => f == null || f.Length > 0)
            .WithMessage("If provided, the image file must not be empty.");
    }
}

