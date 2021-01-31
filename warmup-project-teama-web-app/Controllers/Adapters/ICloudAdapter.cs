using System;
using warmup_project_teama_web_app.Models;

namespace warmup_project_teama_web_app.Controllers.Adapters
{
    /// <summary>
    /// An adapter that will be used to communicate between the web app and the external API.
    /// </summary>
    public interface ICloudAdapter
    {
        /// <summary>
        /// Executes the query by making a remote call to the API
        /// </summary>
        /// <param name="queryParams">A series of params that will be used to query the database</param>
        /// <returns>Response from API, that has been transformed into a TableViewModel object.</returns>
        public TableViewModel execute(string queryParams);

        /// <summary>
        /// Authenticate application with API.
        /// </summary>
        /// <param name="authString">Auth to pass to API for authentication.</param>
        /// <returns>User ID to use for future API calls.</returns>
        public string authenticate(string authString);
    }
}
