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
        /// <param name="userID"> User ID of the user making the query. To be used in the API call.</param>
        /// <param name="authToken">Authentication token to be used in the API call.</param>
        /// <param name="queryParams">A series of params that will be used to query the database</param>
        /// <returns>A response from API that has been transformed into a TableViewModel object.</returns>
        Task<TableViewModel> Execute(string userID, string authToken, ICollection<KVPair> queryParams);

        /// <summary>
        /// Authenticate user of the web app with API.
        /// </summary>
        /// <param name="user_id">Auth to pass to API for authentication.</param>
        /// <returns>Authentication token to use for future API calls.</returns>
        Task<string> Authenticate(string user_id);

        /// <summary>
        /// Helper function for Execute. Converts data from API to feed to ViewModel.
        /// </summary>
        /// <param name="json">JSON object received from API.</param>
        /// <returns>TableViewModel.</returns>
        TableViewModel ToViewModel(List<RootStructure> json);

        /// <summary>
        /// Helper function for Execute. Converts data from list of OtherInfoStructure to 
        /// Dictionary of string, string pairs.
        /// </summary>
        /// <param name="otherInfoList">The list of OtherInfoStructure from the parsed JSON object</param>
        /// <returns>String to format into HTTP request.</returns>
        Dictionary<string, string> ToDictionary(List<OtherInfoStructure> otherInfoList);

        /// <summary>
        /// Helper function for Execute. Converts query from a collection of KVPairs to
        /// a JSON string.
        /// </summary>
        /// <param name="queryParams">Collection of queries from the View.</param>
        /// <returns>A JSON string to be used in the request of the API call.</returns>
        string ToRequestFormat(ICollection<KVPair> queryParams);
    }
}
