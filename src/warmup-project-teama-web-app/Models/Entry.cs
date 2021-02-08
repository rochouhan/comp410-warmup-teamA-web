using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace warmup_project_teama_web_app.Models
{
    /// <summary>
    /// The Entry class represents a single entry in the query data table.
    /// </summary>
    public class Entry
    {
        public string uid; // the user_id of the person who sent the data
        public double latitude; // the latitude part of the location where the data was sent
        public double longitude; // the longitude part of the location where the data was sent
        public DateTime date; // the time when the data was sent
        /// <summary>
        /// A dictionary of any additional info included in the data. The key is the label
        /// and the value is the content of the info.
        /// </summary>
        public Dictionary<string, string> otherinfo;

        /// <summary>
        /// Constructor for the class
        /// </summary>
        /// <param name="uid">see above</param>
        /// <param name="latitude">see above</param>
        /// <param name="longitude">see above</param>
        /// <param name="date">see above</param>
        /// <param name="otherinfo">see above; allowed to be empty</param>
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
