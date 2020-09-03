using DeliverManagementService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryManagementService.IManager
{
    public interface IDeliveryManager
    {
        Task<ResponseModel> MarkOrderAsDelivered(int orderId,string accessToken);
        Task<ResponseModel> MarkOrderAsUndelivered(int orderId,string accessToken);
    }
}
