using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace warmup_project_teama_web_app.Models
{
    /// <summary>
    /// A class that represents a user-inputted key-value pair query
    /// </summary>
    public class KVPair
    {
        /// <summary>
        /// The key for the query e.g. user_id
        /// </summary>
        [JsonProperty(PropertyName = "characteristic")]
        public string Key { get; set; } 
        /// <summary>
        /// The value for the query. Could also be a number
        /// (will get converted in the cloud adapter)
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
        /// <summary>
        /// The operator for the query e.g. eq, lt, gt
        /// </summary>
        [JsonProperty(PropertyName = "operator")]
        public string Op { get; set; }

        /// <summary>
        /// Overrides the ToString for easier readability
        /// </summary>
        /// <returns>A formatted string of the fields</returns>
        public override string ToString()
        {
            return Key + " " + Op + " " + Value;
        }
    }
}
