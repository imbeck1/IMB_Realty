
using MediatR;

public class UploadHouseImageHandler : IRequestHandler<UploadHouseImageRequest, UploadHouseImageRequest.Response>
{
    private readonly HttpClient _httpClient;

    public UploadHouseImageHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UploadHouseImageRequest.Response> Handle(UploadHouseImageRequest request, CancellationToken cancellationToken)
    {
        var fileContent = request.File.OpenReadStream(request.File.Size, cancellationToken);

        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(fileContent), "image", request.File.Name);

        var response = await _httpClient.PostAsync(UploadHouseImageRequest.RouteTemplate.Replace("{houseId}", request.HouseId.ToString()), content, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var fileName = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
            return new UploadHouseImageRequest.Response(fileName);
        }
        else
        {
            return new UploadHouseImageRequest.Response("");
        }
    }
}