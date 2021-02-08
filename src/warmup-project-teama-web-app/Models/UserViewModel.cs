using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace warmup_project_teama_web_app.Models
{
    /// <summary>
    /// Viewmodel that represents a user
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// The user's username (must be authorized to log in)
        /// </summary>
        public string userID { get; set; }
        /// <summary>
        /// The authentication token received when logging in.
        /// Must be passed to the cloud when querying for data in order
        /// to be verified
        /// </summary>
        public string authToken { get; set; }
    }
}
