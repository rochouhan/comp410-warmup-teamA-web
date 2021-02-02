using System;
using warmup_project_teama_web_app.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
//TODO: INSTALL JSON STUFF
//using Newtonsoft.Json;
using System.IO;

namespace warmup_project_teama_web_app.Controllers.Adapters
{
    public class CloudAdapter : ICloudAdapter
    {
        String gotAuthToken;
        static readonly HttpClient client = new HttpClient();

        public CloudAdapter()
        {
        }

        public async Task<string> authenticate(string authString)
        {
            try
            {
                using (var requestMessage =
                    new HttpRequestMessage(HttpMethod.Post, "https://my-resource.azure-api.net/api/CreateNewRecord?name=WebTeam"))
                {
                    requestMessage.Headers.Add("Ocp-Apim-Subscription-Key", authString);
                    HttpResponseMessage response = await client.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseBody);

                    return responseBody;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            
            return null; //TODO
        }

        public async Task<TableViewModel> execute(ICollection<KVPair> queryParams)
        {
            string strQueryParams = queryParams.ToString(); // TODO: FIX THIS!!!

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

                    //TODO: INSTALL JSON STUFF
                    //List<RootStructure> jsonResponse = JsonConvert.DeserializeObject<List<RootStructure>>(responseBody);
                    //System.Diagnostics.Debug.WriteLine(jsonResponse);

                    // return responseBody;

                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return null; //TODD
        }

        public TableViewModel toViewModel(List<RootStructure> json)
        {
            //TODO: INSTALL JSON STUFF
            //JObject data = JObject.Parse(File.ReadAllText("test1.json"));
            
            return new TableViewModel();
        }

        public string fromViewModel()
        {
            return "";
        }

        public async Task Main(string[] args)
        {
            Console.WriteLine("Hello World..!");

            gotAuthToken = await authenticate("83a2f1db11ec471ebf824546a59cfef0");
            //await execute("latitude gt 0");

            //string executeReturn = await Execute("user_id eq user21231");
            System.Diagnostics.Debug.WriteLine("finished");

        }
    }
}
