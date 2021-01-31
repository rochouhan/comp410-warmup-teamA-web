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
            throw new NotImplementedException();
        }

        public TableViewModel execute(string queryParams)
        {
            throw new NotImplementedException();
        }
    }
}
