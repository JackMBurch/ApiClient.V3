using System.Web;

namespace ApiClient.EndpointAPI.ProductInformation
{
    public class RecommendedParts
    {
        private ApiClientService _clientService;

        public ApiClientService ClientService
        {
            get => _clientService;
            set => _clientService = value;
        }

        public RecommendedParts(ApiClientService clientService)
        {
            ClientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        }

        public async Task<string> RecommendedProducts(string digikeyPartNumber, int recordCount = 1)
        {
            var resourcePath = "Recommendations/v3/Products";

            var encodedPN = HttpUtility.UrlEncode(digikeyPartNumber);
            var parameters = $"RecordCount={recordCount}";

            var fullPath = $"/{resourcePath}/{encodedPN}?{parameters}";

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync(fullPath);

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }
    }
}
