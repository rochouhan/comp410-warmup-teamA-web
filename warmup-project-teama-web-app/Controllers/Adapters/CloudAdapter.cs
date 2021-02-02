using System;
using warmup_project_teama_web_app.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;

namespace warmup_project_teama_web_app.Controllers.Adapters
{
    public class CloudAdapter : ICloudAdapter
    {
        static readonly HttpClient client = new HttpClient();

        public CloudAdapter()
        {
        }

        public async Task<bool> Authenticate(string user_id)
        {
            try
            {
                using (var requestMessage =
                    new HttpRequestMessage(HttpMethod.Post, "https://my-resource.azure-api.net/api/auth?user_id=" + user_id))
                {
                    requestMessage.Headers.Add("Ocp-Apim-Subscription-Key", "83a2f1db11ec471ebf824546a59cfef0");
                    HttpResponseMessage response = await client.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    AuthStructure jsonResponse = JsonConvert.DeserializeObject<AuthStructure>(responseBody);
                    System.Diagnostics.Debug.WriteLine(jsonResponse);

                    return jsonResponse.success;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            
            return false; // else case
        }

        public async Task<TableViewModel> Execute(ICollection<KVPair> queryParams)
        {
            List<string> queries = new List<string>();
            
            foreach (KVPair kvpair in queryParams)
            {
                queries.Add(kvpair.ToString());
            }

            // for now just getting first query to make request to API
            string strQueryParams = queries[0];

            string[] splitParams = strQueryParams.Split(' ');
            string chars = splitParams[0];
            string op = splitParams[1];
            string val = splitParams[2];
            string requestString = "https://my-resource.azure-api.net/api/read?characteristic=" + chars + "&operator=" + op + "&value=" + val;

            System.Diagnostics.Debug.WriteLine(requestString);

            try
            {
                using (var requestMessage =
                new HttpRequestMessage(HttpMethod.Get, requestString))
                {
                    requestMessage.Headers.Add("Ocp-Apim-Subscription-Key", "83a2f1db11ec471ebf824546a59cfef0");
                    HttpResponseMessage response = await client.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Console.Write(response);
                    System.Diagnostics.Debug.WriteLine(response);
                    Console.WriteLine("here!");
                    System.Diagnostics.Debug.WriteLine("here yes!");
                    System.Diagnostics.Debug.WriteLine(responseBody);
                    Console.WriteLine(responseBody);

                    List<RootStructure> jsonResponse = JsonConvert.DeserializeObject<List<RootStructure>>(responseBody);
                    System.Diagnostics.Debug.WriteLine(jsonResponse);

                    return ToViewModel(jsonResponse);

                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return null; //TODD
        }

        public TableViewModel ToViewModel(List<RootStructure> json)
        {
            List<Entry> entries = new List<Entry>();

            foreach (RootStructure item in json)
            {
                // TODO: modify when we get final date time format from API group
                string pattern = "MMMM dd h:mmtt";
                DateTime parsedDate;
                string newDateTime = item.time.Substring(0, item.time.Length - 4);

                if (DateTime.TryParseExact(newDateTime, pattern, null, DateTimeStyles.None, out parsedDate))
                {
                    Entry entry = new Entry(item.user_id, parsedDate, ToDictionary(item.other_info));
                    entries.Add(entry);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Unable to convert '{0}' to a date and time.", newDateTime);
                }
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
