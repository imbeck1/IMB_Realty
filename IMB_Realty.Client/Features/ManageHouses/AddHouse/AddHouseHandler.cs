using MediatR;
using System.Net.Http.Json;

public class AddHouseHandler : IRequestHandler<AddHouseRequest, AddHouseRequest.Response>
{
    private readonly HttpClient _httpClient;

    public AddHouseHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AddHouseRequest.Response> Handle(AddHouseRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync(AddHouseRequest.RouteTemplate, request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var houseId = await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken);
            return new AddHouseRequest.Response(houseId);
        }
        else
        {
            return new AddHouseRequest.Response(-1);
        }
    }
}