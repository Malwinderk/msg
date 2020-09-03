using Microsoft.Extensions.Configuration;
using OrderManagementService.IManager;
using OrderManagementService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderManagementService.Service
{
    public class FetchProductDataService: IFetchProductDataService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogManager _logManager;

        public FetchProductDataService(IConfiguration configuration, ILogManager logManager)
        {
            _configuration = configuration;
            _logManager = logManager;
        }
        public async Task<HttpResponseMessage> GetDataFromOrderManagementService(string apiUrl, string accessToken)
        {
            string url = _configuration.GetValue<string>("OMSEndpoint") + apiUrl;
            _logManager.LogDebug($"for fetching product data {url}");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                return await client.GetAsync(url);
            }
        }
    }
}
