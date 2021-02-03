using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace warmup_project_teama_web_app.Models
{
    public class Entry
    {
        public string uid;
        public double latitude;
        public double longitude;
        public DateTime date;
        public Dictionary<string, string> otherinfo;

        public Entry(string uid, DateTime date, Dictionary<string, string> otherinfo)
        {
            this.uid = uid;
            this.date = date;
            this.otherinfo = otherinfo;
        }

        public Entry(string uid, double latitude, double longitude, DateTime date, Dictionary<string, string> otherinfo)
        {
            this.uid = uid;
            this.date = date;
            this.latitude = latitude;
            this.longitude = longitude;
            this.otherinfo = otherinfo;
        }

    }
}
