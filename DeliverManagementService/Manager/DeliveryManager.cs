using DeliverManagementService.Dtos;
using DeliverManagementService.IService;
using DeliveryManagementService.IManager;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryManagementService.Manager
{
    public class DeliveryManager : IDeliveryManager
    {
        private readonly IConfiguration _configuration;
        private readonly ILogManager _logManager;
        private readonly IOrderUpdateService _orderUpdateService;

        public DeliveryManager(IConfiguration configuration, ILogManager logManager, IOrderUpdateService orderUpdateService)
        {
            _configuration = configuration;
            _logManager = logManager;
            _orderUpdateService = orderUpdateService;
        }
        public async Task<ResponseModel> MarkOrderAsDelivered(int orderId, string accessToken)
        {
            try
            {
                _logManager.LogDebug($"mark order as delivered: {orderId}");
                var apiUrl = _configuration.GetValue<string>("MarkDelivered") + orderId;
                var responseMessage = await _orderUpdateService.GetDataFromOrderManagementService(apiUrl, accessToken);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = await responseMessage.Content.ReadAsStringAsync();
                    var deliverResult = JsonConvert.DeserializeObject<ResponseModel>(response);
                    return deliverResult;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logManager.LogExceptionAndInnerException(ex);
                return null;
            }
        }

        public async Task<ResponseModel> MarkOrderAsUndelivered(int orderId, string accessToken)
        {
            try
            {
                _logManager.LogDebug($"mark order as delivered: {orderId}");
                var apiUrl = _configuration.GetValue<string>("MarkUndelivered") + orderId;
                var responseMessage = await _orderUpdateService.GetDataFromOrderManagementService(apiUrl, accessToken);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = await responseMessage.Content.ReadAsStringAsync();
                    var deliveryResult = JsonConvert.DeserializeObject<ResponseModel>(response);
                    return deliveryResult;
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
