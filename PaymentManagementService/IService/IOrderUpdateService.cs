using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentManagementService.IService
{
    public interface IOrderUpdateService
    {
        Task<HttpResponseMessage> GetDataFromOrderManagementService(string apiUrl);
        Task<HttpResponseMessage> PostDataToOrderManagementService(string apiUrl, string requestBody, string accessToken);
    }
}
