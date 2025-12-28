using MediatR;
using System.Net.Http.Json;

public class GetHouseHandler : IRequestHandler<GetHouseRequest, GetHouseRequest.Response?>
{
    private readonly HttpClient _httpClient;

    public GetHouseHandler(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<GetHouseRequest.Response?> Handle(GetHouseRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<GetHouseRequest.Response>(
                $"/api/houses/{request.HouseId}", cancellationToken);
        }
        catch
        {
            return null;
        }
    }
}
