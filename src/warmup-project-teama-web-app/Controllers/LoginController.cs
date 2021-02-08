using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warmup_project_teama_web_app.Controllers.Adapters;
using warmup_project_teama_web_app.Models;
using warmup_project_teama_web_app.Models.Session;

namespace warmup_project_teama_web_app.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// A static cloud adapter instance used to communicate between
        /// the controller and the cloud adapter
        /// </summary>
        private ICloudAdapter cloudAdapter = new CloudAdapter();

        /// <summary>
        /// Default login page. Features a login box.
        /// </summary>
        /// <returns>Login page with an empty UserViewModel</returns>
        public IActionResult Index()
        {
            return View(new UserViewModel());
        }

        /// <summary>
        /// Takes in a user with a user-inputted username and
        /// authenticates the user. If successful, saves the user in the current
        /// session and redirects to the home page. Else, refreshes the page.
        /// </summary>
        /// <param name="user">A UserViewModel instance with a non-null username</param>
        /// <returns>Either the home page or the login page (accepted/rejected)</returns>
        [HttpPost]
        public IActionResult Index(UserViewModel user)
        {
            if (ModelState.IsValid && user.userID != null)
            {
                string authToken = cloudAdapter.Authenticate(user.userID).Result;
                if (authToken != null)
                {
                    user.authToken = authToken;
                    HttpContext.Session.Set<UserViewModel>("user", user);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }
    }
}
