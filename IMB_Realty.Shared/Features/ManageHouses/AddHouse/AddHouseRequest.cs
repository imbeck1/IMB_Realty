using System.Net;
using FluentValidation;
using IMB_Realty.Shared.Features.ManageHouses;
using MediatR;
using Microsoft.AspNetCore.Http;

public record AddHouseRequest(HouseDto House, IFormFile? Image) : IRequest<AddHouseRequest.Response>
{
    public const string RouteTemplate = "/api/houses";

    public record Response(int HouseId);
}

public class AddHouseRequestValidator : AbstractValidator<AddHouseRequest>
{
    public AddHouseRequestValidator()
    {
        RuleFor(x => x.House).SetValidator(new HouseValidator());

        // Optional: validate image file
        RuleFor(x => x.Image)
            .Must(f => f == null || f.Length > 0)
            .WithMessage("If provided, the image file must not be empty.");
    }
}
