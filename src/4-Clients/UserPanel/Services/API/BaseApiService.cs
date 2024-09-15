using TaskoMask.BuildingBlocks.Web.Services.Http;
using TaskoMask.Clients.UserPanel.Helpers;

namespace TaskoMask.Clients.UserPanel.Services.API;

public abstract class BaseApiService
{
    protected readonly IHttpClientService HttpClientService;

    public BaseApiService(IHttpClientService httpClientService, string clientName = MagicKey.PROTECTED_APIGATEWAY_CLIENT)
    {
        HttpClientService = httpClientService.WithNamedClient(clientName);
    }
}
