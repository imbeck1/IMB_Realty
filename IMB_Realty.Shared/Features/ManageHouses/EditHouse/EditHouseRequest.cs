using FluentValidation;
using IMB_Realty.Shared.Features.ManageHouses;
using MediatR;

public record EditHouseRequest(HouseDto House) : IRequest<EditHouseRequest.Response>
{
    public const string RouteTemplate = "/api/houses/";

    public record Response(bool IsSuccess);
}

public class EditHouseRequestValidator : AbstractValidator<EditHouseRequest>
{
    public EditHouseRequestValidator()
    {
        RuleFor(x => x.House).SetValidator(new HouseValidator());
    }
}
