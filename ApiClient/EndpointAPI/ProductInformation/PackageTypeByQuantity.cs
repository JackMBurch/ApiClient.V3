using System.Web;

namespace ApiClient.EndpointAPI.ProductInformation
{
    public class PackageTypeByQuantity
    {
        private ApiClientService _clientService;

        public ApiClientService ClientService
        {
            get => _clientService;
            set => _clientService = value;
        }

        public PackageTypeByQuantity(ApiClientService clientService)
        {
            ClientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        }

        public async Task<string> PackageByQuantity(string digikeyPartNumber, int requestedQuantity)
        {
            var resourcePath = "PackageTypeByQuantity/v3/Products";

            var encodedPN = HttpUtility.UrlEncode(digikeyPartNumber);
            var parameters = $"requestedQuantity={requestedQuantity}";

            var fullPath = $"/{resourcePath}/{encodedPN}?{parameters}";

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync(fullPath);

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }
    }
}
