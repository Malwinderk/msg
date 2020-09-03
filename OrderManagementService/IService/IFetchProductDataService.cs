using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderManagementService.IService
{
    public interface IFetchProductDataService
    {
        Task<HttpResponseMessage> GetDataFromOrderManagementService(string apiUrl, string accessToken);
    }
}
