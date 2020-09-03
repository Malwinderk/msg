using DeliverManagementService.IService;
using DeliveryManagementService.IManager;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeliverManagementService.Service
{
    public class OrderUpdateService:IOrderUpdateService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogManager _logManager;

        public OrderUpdateService(IConfiguration configuration, ILogManager logManager)
        {
            _configuration = configuration;
            _logManager = logManager;
        }

        public async Task<HttpResponseMessage> GetDataFromOrderManagementService(string apiUrl,string accessToken)
        {
            string url = _configuration.GetValue<string>("OMSEndpoint") + apiUrl;
            _logManager.LogDebug($"sending request to makr order as paid to {url}");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                return await client.GetAsync(url);
            }
        }
    }
}
