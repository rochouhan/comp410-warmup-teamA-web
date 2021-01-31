using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace warmup_project_teama_web_app.Models
{
    public class Entry
    {
        public string uid;
        public DateTime date;
        public Dictionary<string, string> otherinfo;

        public Entry(string uid, DateTime date, Dictionary<string, string> otherinfo)
        {
            this.uid = uid;
            this.date = date;
            this.otherinfo = otherinfo;
        }

    }
}
