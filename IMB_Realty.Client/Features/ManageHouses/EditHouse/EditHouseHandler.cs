using MediatR;
using System.Net.Http.Json;

public class EditHouseHandler : IRequestHandler<EditHouseRequest, EditHouseRequest.Response>
{
    private readonly HttpClient _httpClient;

    public EditHouseHandler(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<EditHouseRequest.Response> Handle(EditHouseRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PutAsJsonAsync(EditHouseRequest.RouteTemplate, request, cancellationToken);

        return new EditHouseRequest.Response(response.IsSuccessStatusCode);
    }
}
