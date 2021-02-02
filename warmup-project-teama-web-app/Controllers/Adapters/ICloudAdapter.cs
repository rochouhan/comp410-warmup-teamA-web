using System;
using System.Collections.Generic;
using warmup_project_teama_web_app.Models;
using System.Threading.Tasks;

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
        /// <returns>A response from API that has been transformed into a TableViewModel object.</returns>
        Task<TableViewModel> execute(ICollection<KVPair> queryParams);

        /// <summary>
        /// Authenticate application with API.
        /// </summary>
        /// <param name="authString">Auth to pass to API for authentication.</param>
        /// <returns>User ID to use for future API calls.</returns>
        Task<string> authenticate(string authString);

        /// <summary>
        /// Convert data from API to feed to ViewModel.
        /// </summary>
        /// <param name="json">JSON object received from API.</param>
        /// <returns>TableViewModel.</returns>
        TableViewModel toViewModel(List<RootStructure> json);

        /// <summary>
        /// Convert data from ViewModel to API.
        /// </summary>
        /// <param name="authString">tbd.</param>
        /// <returns>String to format into HTTP request.</returns>
        string fromViewModel();
    }
}
