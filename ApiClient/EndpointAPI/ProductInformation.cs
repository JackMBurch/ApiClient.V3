using ApiClient.Models;
using System.Web;

namespace ApiClient.EndpointAPI
{
    public class ProductInformation
    {
        private ApiClientService _clientService;

        public ApiClientService ClientService
        {
            get => _clientService;
            set => _clientService = value;
        }

        public ProductInformation(ApiClientService clientService)
        {
            ClientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        }

        #region PartSearch

        public async Task<string> KeywordSearch(string keyword)
        {
            var resourcePath = "/Search/v3/Products/Keyword";

            var request = new KeywordSearchRequest
            {
                Keywords = keyword ?? "P5555-ND",
                RecordCount = 25
            };

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var postResponse = await _clientService.PostAsJsonAsync(resourcePath, request);

            return ApiClientService.GetServiceResponse(postResponse).Result;
        }

        public async Task<string> ProductDetails(string digikeyPartNumber)
        {
            var resourcePath = "Search/v3/Products";

            var encodedPN = HttpUtility.UrlEncode(digikeyPartNumber);

            var fullPath = $"/{resourcePath}/{encodedPN}";

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync(fullPath);

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }

        public async Task<string> DigiReelPricing(string digiKeyPartNumber, int requestedQuantity)
        {
            var resourcePathPrefix = "Search/v3/Products";
            var resourcePathSuffix = "DigiReelPricing";

            var encodedPN = HttpUtility.UrlEncode(digiKeyPartNumber);
            var parameters = $"requestedQuantity={requestedQuantity}";

            var fullPath = $"/{resourcePathPrefix}/{encodedPN}/{resourcePathSuffix}?{parameters}";

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync(fullPath);

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }

        public async Task<string> SuggestedParts(string partNumber)
        {
            var resourcePathPrefix = "Search/v3/Products";
            var resourcePathSuffix = "WithSuggestedProducts";

            var encodedPN = HttpUtility.UrlEncode(partNumber);

            var fullPath = $"/{resourcePathPrefix}/{encodedPN}/{resourcePathSuffix}";

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync(fullPath);

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }

        public async Task<string> Manufacturers()
        {
            var resourcePath = "Search/v3/Manufacturers";

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync($"{resourcePath}");

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }

        public async Task<string> Categories()
        {
            var resourcePath = "Search/v3/Categories";

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync($"{resourcePath}");

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }

        public async Task<string> CategoriesByID(int categoryID)
        {
            var resourcePathPrefix = "Search/v3/Categories";

            var fullPath = $"/{resourcePathPrefix}/{categoryID}";

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync($"{fullPath}");

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }

        #endregion

        #region RecommendedParts

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

        #endregion

        #region PackageTypeByQuantity

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

        #endregion

        #region ProductChangeNotifications



        #endregion

        #region ProductTracing



        #endregion
    }
}
