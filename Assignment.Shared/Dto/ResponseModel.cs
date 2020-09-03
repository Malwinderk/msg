using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Shared.Dto
{
    public class ResponseModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Data { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}
