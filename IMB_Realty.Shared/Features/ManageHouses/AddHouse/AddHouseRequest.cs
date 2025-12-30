using System.Net;
using FluentValidation;
using IMB_Realty.Shared.Features.ManageHouses;
using MediatR;

namespace IMB_Realty.Shared.Features.ManageHouses;

public record AddHouseRequest(HouseDto House) : IRequest<AddHouseRequest.Response>
{
    public const string RouteTemplate = "/api/houses";

    public record Response(int HouseId);
}


public class AddHouseRequestValidator : AbstractValidator<AddHouseRequest>
{
    public AddHouseRequestValidator()
    {
        RuleFor(x => x.House).SetValidator(new HouseValidator());
    }
}

