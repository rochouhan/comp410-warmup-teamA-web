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
    public class CloudAdapter : ICloudAdapter
    {

        static readonly HttpClient client = new HttpClient();

        public CloudAdapter()
        {
        }

        public async Task<string> Authenticate(string user_id)
        {
            try
            {
                using (var requestMessage =
                    new HttpRequestMessage(HttpMethod.Post, "https://teamafrontendapi.azure-api.net/api/auth?user_id=" + user_id))
                {
                    // user_id included in body
                    requestMessage.Content = new StringContent(
                        JsonConvert.SerializeObject(new { user_id }),
                        Encoding.UTF8, "application/json");


                    HttpResponseMessage response = await client.SendAsync(requestMessage);
                    // System.Diagnostics.Debug.WriteLine(response);
                    // System.Diagnostics.Debug.WriteLine("===============================");
                    // System.Diagnostics.Debug.WriteLine(response.EnsureSuccessStatusCode());
                    // System.Diagnostics.Debug.WriteLine("===============================");
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        AuthStructure jsonResponse = JsonConvert.DeserializeObject<AuthStructure>(responseBody);
                        System.Diagnostics.Debug.WriteLine(responseBody);
                        System.Diagnostics.Debug.WriteLine(jsonResponse.token);
                        return jsonResponse.token;
                    } else {
                        return null;
                    }
                    //return jsonResponse.success;
                }
            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine("\nException Caught!");
                System.Diagnostics.Debug.WriteLine("Message :{0} ", e.Message);
            }
            
            return null; // else case
        }

        public async Task<TableViewModel> Execute(string user_id, string authToken, ICollection<KVPair> queryParams)
        {
            //foreach (KVPair kvpair in queryParams)
            //{
            //    queries.Add(kvpair.ToString());
            //}

            // for now just getting first query to make request to API
            System.Diagnostics.Debug.WriteLine("TOKEN:");
            System.Diagnostics.Debug.WriteLine(authToken);
            KVPair[] queryParamsList = new KVPair[queryParams.Count];
            queryParams.CopyTo(queryParamsList, 0);
            KVPair firstKVPair = queryParamsList[0];

            string chars = firstKVPair.Key;
            string op = firstKVPair.Op;
            string val = firstKVPair.Value;
            //string requestString = "https://my-resource.azure-api.net/api/read?user_id=" + userID + "&characteristic=" + chars + "&operator=" + op + "&value=" + val;

            // current version of the API endpoint for now
            // string requestString = "https://my-resource.azure-api.net/v3/api/ReadFunction?" + chars + "=" + val;
            string requestString = "https://teamafrontendapi.azure-api.net/api/read?user_id=" + user_id + "&characteristic=" + chars + "&operator=" + op + "&value=" + val;
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

                    System.Diagnostics.Debug.WriteLine("RESPONSE===================================================");
                    System.Diagnostics.Debug.WriteLine(response);
                    System.Diagnostics.Debug.WriteLine("RESPONSE BODY==============================================");
                    System.Diagnostics.Debug.WriteLine(responseBody);

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

        public Dictionary<string, string> ToDictionary(List<OtherInfoStructure> otherInfoList)
        {
            Dictionary<string, string> otherInfoDict = new Dictionary<string, string>();

            foreach (OtherInfoStructure otherInfo in otherInfoList)
            {
                otherInfoDict[otherInfo.contentType] = otherInfo.value;
            }

            return otherInfoDict;
        }

        public string ToRequestFormat()
        {
            return "";
        }

        //public async Task Main(string[] args)
        //{
            //Console.WriteLine("Hello World..!");

            //gotAuthToken = await Authenticate("83a2f1db11ec471ebf824546a59cfef0");

            //string executeReturn = await Execute("user_id eq user21231");
            //System.Diagnostics.Debug.WriteLine("finished");

        //}
    }
}
