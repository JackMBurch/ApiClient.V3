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

        public async Task<string> KeywordSearch(string keyword, string[]? includes = null)
        {
            var resourcePath = "Search/v3/Products/Keyword";

            var parameters = string.Empty;

            if (includes != null)
            {
                var includesString = HttpUtility.UrlEncode(string.Join(",", includes));
                parameters += $"?includes={includesString}";
            }

            var fullPath = $"/{resourcePath}{parameters}";

            var request = new KeywordSearchRequest
            {
                Keywords = keyword ?? "P5555-ND",
                RecordCount = 25
            };

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var postResponse = await _clientService.PostAsJsonAsync(fullPath, request);

            return ApiClientService.GetServiceResponse(postResponse).Result;
        }

        public async Task<string> ProductDetails(string digikeyPartNumber, string[]? includes = null)
        {
            var resourcePath = "Search/v3/Products";

            var parameters = string.Empty;

            if (includes != null)
            {
                var includesString = HttpUtility.UrlEncode(string.Join(",", includes));
                parameters += $"?includes={includesString}";
            }

            var encodedPN = HttpUtility.UrlEncode(digikeyPartNumber);

            var fullPath = $"/{resourcePath}/{encodedPN}{parameters}";

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync(fullPath);

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }

        public async Task<string> DigiReelPricing(string digiKeyPartNumber, int requestedQuantity, string[]? includes = null)
        {
            var resourcePathPrefix = "Search/v3/Products";
            var resourcePathSuffix = "DigiReelPricing";

            var encodedPN = HttpUtility.UrlEncode(digiKeyPartNumber);

            var parameters = $"requestedQuantity={requestedQuantity}";

            if (includes != null)
            {
                var includesString = HttpUtility.UrlEncode(string.Join(",", includes));
                parameters += $"&includes={includesString}";
            }

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

        public async Task<string> RecommendedProducts(string digikeyPartNumber, int recordCount = 1, string[]? searchOptionList = null, bool excludeMarketPlaceProducts = false, string[]? includes = null)
        {
            var resourcePath = "Recommendations/v3/Products";

            var encodedPN = HttpUtility.UrlEncode(digikeyPartNumber);

            var parameters = $"recordCount={recordCount}";

            if (searchOptionList != null)
            {
                var optionListString = HttpUtility.UrlEncode(string.Join(",", searchOptionList));
                parameters += $"&searchOptionList={optionListString}";
            }

            if (excludeMarketPlaceProducts == true)
            {
                parameters += $"&excludeMarketPlaceProducts=true";
            }

            if (includes != null)
            {
                var includesString = HttpUtility.UrlEncode(string.Join(",", includes));
                parameters += $"&includes={includesString}";
            }

            var fullPath = $"/{resourcePath}/{encodedPN}?{parameters}";

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync(fullPath);

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }

        #endregion

        #region PackageTypeByQuantity

        public async Task<string> PackageByQuantity(string digikeyPartNumber, int requestedQuantity, string packagingPreference = null, string[]? includes = null)
        {
            var resourcePath = "PackageTypeByQuantity/v3/Products";

            var encodedPN = HttpUtility.UrlEncode(digikeyPartNumber);

            var parameters = HttpUtility.UrlEncode($"requestedQuantity={requestedQuantity}");

            if (packagingPreference != null)
            {
                parameters += $"&packagingPreference={HttpUtility.UrlEncode(packagingPreference)}";
            }

            if (includes != null)
            {
                var includesString = HttpUtility.UrlEncode(string.Join(",", includes));
                parameters += $"&includes={includesString}";
            }

            var fullPath = $"/{resourcePath}/{encodedPN}?{parameters}";

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync(fullPath);

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }

        #endregion

        #region ProductChangeNotifications

        public async Task<string> ProductChangeNotifications(string digikeyPartNumber, string[]? includes = null)
        {
            var resourcePath = "ChangeNotifications/v3/Products";

            var encodedPN = HttpUtility.UrlEncode(digikeyPartNumber);

            string fullPath;

            if (includes == null)
                fullPath = $"/{resourcePath}/{encodedPN}";

            else
            {
                var includesString = HttpUtility.UrlEncode(string.Join(",", includes));
                var parameters = $"inlcudes={includesString}";
                fullPath = $"/{resourcePath}/{encodedPN}?{parameters}";
            }

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync(fullPath);

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }

        #endregion

        #region ProductTracing

        public async Task<string> ProductTracingDetails(string tracingID)
        {
            var resourcePath = "ProductTracing/v1/Details";

            var encodedID = HttpUtility.UrlEncode(tracingID);

            var fullPath = $"/{resourcePath}/{encodedID}";

            await _clientService.ResetExpiredAccessTokenIfNeeded();
            var getResponse = await _clientService.GetAsync(fullPath);

            return ApiClientService.GetServiceResponse(getResponse).Result;
        }

        #endregion
    }
}
