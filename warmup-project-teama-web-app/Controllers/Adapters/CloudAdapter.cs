using System;
using warmup_project_teama_web_app.Models;

namespace warmup_project_teama_web_app.Controllers.Adapters
{
    public class CloudAdapter : ICloudAdapter
    {
        public CloudAdapter()
        {
        }

        public string authenticate(string authString)
        {
            return null; //TODO
        }

        public TableViewModel execute(string queryParams)
        {
            return null; //TODO
        }
    }
}
