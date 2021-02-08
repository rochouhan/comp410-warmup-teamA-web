using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using warmup_project_teama_web_app.Controllers.Adapters;
using warmup_project_teama_web_app.Models;
using warmup_project_teama_web_app.Models.Session;

namespace warmup_project_teama_web_app.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// A static cloud adapter instance used to communicate between
        /// the controller and the cloud adapter
        /// </summary>
        private ICloudAdapter cloudAdapter = new CloudAdapter();

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public HomeController()
        {
        }

        /// <summary>
        /// Default home page. Features an empty query and data table.
        /// </summary>
        /// <returns>Home page with an empty TableViewModel</returns>
        public IActionResult Index()
        {
            UserViewModel user = getUserFromSession();
            if (user == default | user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["userID"] = user.userID;
            return View(new TableViewModel());
        }

        /// <summary>
        /// Takes in query parameters and updates the table in the view
        /// </summary>
        /// <param name="dataList">A list of query parameters</param>
        /// <returns>An updated view with a filled TableViewModel</returns>
        [HttpPost]
        public IActionResult Index(ICollection<KVPair> dataList)
        {
            UserViewModel user = getUserFromSession();
            if (user == default | user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["userID"] = user.userID;
            if (ModelState.IsValid && dataList.Count > 0)
            {
                TableViewModel entries = cloudAdapter.Execute(user.userID, user.authToken, dataList).Result;
                return View(entries);
            }
            return View(new TableViewModel());
        }

        /// <summary>
        /// Shorthand function for getting the user from the current session
        /// </summary>
        /// <returns>The UserViewModel info of the current user</returns>
        private UserViewModel getUserFromSession()
        {
            return HttpContext.Session.Get<UserViewModel>("user");
        }
    }
}