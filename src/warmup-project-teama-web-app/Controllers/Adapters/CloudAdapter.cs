using System;
using warmup_project_teama_web_app.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace warmup_project_teama_web_app.Controllers.Adapters
{
    /// <summary>
    /// An adapter that will be used to communicate between the web app and the external API.
    /// </summary>
    public class CloudAdapter : ICloudAdapter
    {

        static readonly HttpClient client = new HttpClient();

        public CloudAdapter()
        {
        }

        /// <summary>
        /// Authenticate user of the web app with API.
        /// </summary>
        /// <param name="user_id">Auth to pass to API for authentication.</param>
        /// <returns>Authentication token to use for future API calls.</returns>
        public async Task<string> Authenticate(string user_id)
        {
            try
            {
                using (var requestMessage =
                    new HttpRequestMessage(HttpMethod.Post, "https://teamafrontendapi.azure-api.net/v2/api/AuthFunction?user_id=" + user_id))
                {
                    requestMessage.Content = new StringContent(
                        JsonConvert.SerializeObject(new { user_id }),
                        Encoding.UTF8, "application/json");


                    HttpResponseMessage response = await client.SendAsync(requestMessage);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        AuthStructure jsonResponse = JsonConvert.DeserializeObject<AuthStructure>(responseBody);
                        return jsonResponse.token;
                    } else {
                        return null;
                    }
                }
            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine("\nException Caught!");
                System.Diagnostics.Debug.WriteLine("Message :{0} ", e.Message);
            }
            
            return null;
        }


        /// <summary>
        /// Executes the query by making a remote call to the API
        /// </summary>
        /// <param name="userID"> User ID of the user making the query. To be used in the API call.</param>
        /// <param name="authToken">Authentication token to be used in the API call.</param>
        /// <param name="queryParams">A series of params that will be used to query the database</param>
        /// <returns>A response from API that has been transformed into a TableViewModel object.</returns>
        public async Task<TableViewModel> Execute(string user_id, string authToken, ICollection<KVPair> queryParams)
        {
            string paramsString = ToRequestFormat(queryParams);
            string requestString = "https://teamafrontendapi.azure-api.net/v2/api/ReadFunction?user_id=" + user_id + "&query=" + paramsString;
            System.Diagnostics.Debug.WriteLine(requestString);

            try
            {
                using (var requestMessage =
                new HttpRequestMessage(HttpMethod.Get, requestString))
                {
                    requestMessage.Headers.Add("Authorization", authToken);
                    HttpResponseMessage response = await client.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    List<RootStructure> jsonResponse = JsonConvert.DeserializeObject<List<RootStructure>>(responseBody);

                    return ToViewModel(jsonResponse);
                }
            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine("\nException Caught!");
                System.Diagnostics.Debug.WriteLine("Message :{0} ", e.Message);
            }

            return new TableViewModel();
        }

        /// <summary>
        /// Helper function for Execute. Converts data from API to feed to ViewModel.
        /// </summary>
        /// <param name="json">JSON object received from API.</param>
        /// <returns>TableViewModel.</returns>
        public TableViewModel ToViewModel(List<RootStructure> json)
        {
            // list of entries for the ViewModel
            List<Entry> entries = new List<Entry>();

            foreach (RootStructure item in json)
            {
                // converting milliseconds into DateTime
                DateTime date = new DateTime(1000 * item.time);

                // empty dictionary for otherInfo in the event that item.otherInfo is null
                Dictionary<string, string> otherInfoDict = new Dictionary<string, string>();
                
                if (item.other_info != null)
                {
                    otherInfoDict = ToDictionary(item.other_info);
                } 

                Entry entry = new Entry(item.user_id, item.latitude, item.longitude, date, otherInfoDict);
                entries.Add(entry);

            }

            return new TableViewModel(entries);
        }

        /// <summary>
        /// Helper function for Execute. Converts data from list of OtherInfoStructure to 
        /// Dictionary of string, string pairs.
        /// </summary>
        /// <param name="otherInfoList">The list of OtherInfoStructure from the parsed JSON object</param>
        /// <returns>String to format into HTTP request.</returns>
        public Dictionary<string, string> ToDictionary(List<OtherInfoStructure> otherInfoList)
        {
            Dictionary<string, string> otherInfoDict = new Dictionary<string, string>();

            foreach (OtherInfoStructure otherInfo in otherInfoList)
            {
                string key = otherInfo.name + " (" + otherInfo.contentType + ")";
                otherInfoDict[key] = otherInfo.value;
            }

            return otherInfoDict;
        }

        /// <summary>
        /// Helper function for Execute. Converts query from a collection of KVPairs to
        /// a JSON string.
        /// </summary>
        /// <param name="queryParams">Collection of queries from the View.</param>
        /// <returns>A JSON string to be used in the request of the API call.</returns>
        public string ToRequestFormat(ICollection<KVPair> queryParams)
        {
            var count = 0;
            var len = queryParams.Count;
            var paramsString = "[";
            foreach (var pair in queryParams)
            {
                count++;
                paramsString += "{'characteristic': '" + pair.Key + "', 'operator': '" + pair.Op + "', 'value': ";
                try
                {
                    Convert.ToDouble(pair.Value);
                    paramsString += pair.Value + "}";
                }
                catch (FormatException)
                {
                    paramsString += "'" + pair.Value + "'}";
                }
                if (count < len)
                {
                    paramsString += ", ";
                }
            }
            paramsString += "]";

            return paramsString;
        }
    }
}
