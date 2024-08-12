using Fusion_Cache_Lib.Services.Fusion_Cache;
using Fusion_Cache_Lib.Services.Request.Dummy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZiggyCreatures.Caching.Fusion;

namespace Fusion_Cache_Lib.Services.DummyService
{
    public interface IDummyService
    {
        Task<ApiResponse> GetEmployees();
    }
    public class DummyService : IDummyService
    {
        private readonly IFusionCacheHelper _fusionCache;
        private readonly HttpClient _httpClient;

        public DummyService( IFusionCacheHelper fusionCacheHelper,IHttpClientFactory  httpClientFactory)
        {
            _fusionCache = fusionCacheHelper;
			_httpClient = httpClientFactory.CreateClient("dummyapi");
		}
        public async Task<ApiResponse> GetEmployees()
        {
            ApiResponse apiResponse = new();

            try
            {

                apiResponse = await _fusionCache.GetCache<ApiResponse>("emp");

                if(apiResponse is null)
                {

					using (HttpClient client =_httpClient)
					{
						HttpResponseMessage httpResponseMessage = await client.GetAsync("employees");

						if (httpResponseMessage.IsSuccessStatusCode)
						{
							string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
							apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

							await _fusionCache.SetCache("emp", apiResponse);
						}
						else
						{
							// Handle the case where the response is not successful
							apiResponse.Message = $"Request failed with status code {httpResponseMessage.StatusCode}";
						}
					}
				}
            }
            catch (Exception ex)
            {
                // Handle exceptions
                apiResponse.Message = $"An error occurred: {ex.Message}";
            }

            return apiResponse;
        }

    }
}
