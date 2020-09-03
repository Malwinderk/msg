using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeliverManagementService.IService
{
    public interface IOrderUpdateService
    {
        Task<HttpResponseMessage> GetDataFromOrderManagementService(string apiUrl, string accessToken);
    }
}
