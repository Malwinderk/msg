using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaymentManagementService.Dtos;
using PaymentManagementService.IManager;
using PaymentManagementService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentManagementService.Manager
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IOrderUpdateService _orderUpdateService;
        private readonly IConfiguration _configuration;
        private readonly ILogManager _logManager;

        public PaymentManager(IOrderUpdateService orderUpdateService, IConfiguration configuration,ILogManager logManager)
        {
            _orderUpdateService = orderUpdateService;
            _configuration = configuration;
            _logManager = logManager;
        }
        public async Task<ResponseModel> MakePayment(OrderPayment orderPayment,string accessToken)
        {
            try
            {
                _logManager.LogDebug($"make payment for order: {orderPayment.OrderId}");
                var stringPayload = Task.Run(() => JsonConvert.SerializeObject(orderPayment)).Result; //try sending with serializeobject
                var apiUrl = _configuration.GetValue<string>("MakePayment");
                var responseMessage = await _orderUpdateService.PostDataToOrderManagementService(apiUrl, stringPayload, accessToken);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = await responseMessage.Content.ReadAsStringAsync();
                    var paymentResult = JsonConvert.DeserializeObject<ResponseModel>(response);
                    return paymentResult;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logManager.LogExceptionAndInnerException(ex);
                return null;
            }
        }
    
    }
}
