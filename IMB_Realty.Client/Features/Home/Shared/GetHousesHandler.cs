using MediatR;
using System.Net.Http.Json;
using IMB_Realty.Shared.Features.Home.Shared.GetHousesRequest;

public class GetHousesHandler : IRequestHandler<GetHousesRequest, GetHousesRequest.Response?>
{
    private readonly HttpClient _httpClient;

    public GetHousesHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetHousesRequest.Response?> Handle(GetHousesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<GetHousesRequest.Response>(GetHousesRequest.RouteTemplate);
        }
        catch (HttpRequestException)
        {
            return default!;
        }
    }
}