using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace warmup_project_teama_web_app.Models
{
    public class KVPair
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Op { get; set; }

        
        public override string ToString()
        {
            return Key + " " + Op + " " + Value;
        }
    }
}
