using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.InventoryService.Dtos
{
    public class ResponseModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Data { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}
