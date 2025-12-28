using MediatR;
using Microsoft.AspNetCore.Components.Forms;

public record UploadHouseImageRequest(int HouseId, IBrowserFile File) : IRequest<UploadHouseImageRequest.Response>
{
    public const string RouteTemplate = "/api/houses/{houseId}/images";

    public record Response(string ImageName);
}