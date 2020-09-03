using PaymentManagementService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentManagementService.IManager
{
    public interface IPaymentManager
    {
        Task<ResponseModel> MakePayment(OrderPayment orderPayment, string accessToken);
    }
}
