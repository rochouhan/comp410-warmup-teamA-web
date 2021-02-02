using System;
using warmup_project_teama_web_app.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace warmup_project_teama_web_app.Controllers.Adapters
{
    public class CloudAdapter : ICloudAdapter
    {
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


        public TableViewModel execute(ICollection<KVPair> queryParams)
        {
            return new TableViewModel(); //TODO
        }

        public async Task Main(string[] args)
        {
            Console.WriteLine("Hello World..!");

            await authenticate("83a2f1db11ec471ebf824546a59cfef0");

        }
    }
}
