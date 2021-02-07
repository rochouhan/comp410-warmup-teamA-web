using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace warmup_project_teama_web_app.Models
{
    public class KVPair
    {
        [JsonProperty(PropertyName = "characteristic")]
        public string Key { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
        [JsonProperty(PropertyName = "operator")]
        public string Op { get; set; }

        
        public override string ToString()
        {
            return Key + " " + Op + " " + Value;
        }
    }
}
