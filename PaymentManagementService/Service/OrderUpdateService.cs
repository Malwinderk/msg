using Microsoft.Extensions.Configuration;
using PaymentManagementService.IManager;
using PaymentManagementService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementService.Service
{
    public class OrderUpdateService: IOrderUpdateService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogManager _logManager;

        public OrderUpdateService(IConfiguration configuration,ILogManager logManager)
        {
            _configuration = configuration;
            _logManager = logManager;
        }

        public async Task<HttpResponseMessage> GetDataFromOrderManagementService(string apiUrl)
        {
            string url = _configuration.GetValue<string>("OMSEndpoint") + apiUrl;
            using (HttpClient client = new HttpClient())
            {
                return await client.GetAsync(url);
            }
        }

        public async Task<HttpResponseMessage> PostDataToOrderManagementService(string apiUrl, string requestBody,string accessToken)
        {
            string url = _configuration.GetValue<string>("OMSEndpoint") + apiUrl;
            _logManager.LogDebug($"sending request to makr order as paid to {url}");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                return await client.PostAsync(url, new StringContent(requestBody, Encoding.UTF8, "application/json"));
            }
        }
    }
}
