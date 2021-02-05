using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warmup_project_teama_web_app.Controllers.Adapters;
using warmup_project_teama_web_app.Models;
using warmup_project_teama_web_app.Models.Session;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace warmup_project_teama_web_app.Controllers
{
    public class LoginController : Controller
    {
        // Should we make a singleton of this? 
        private ICloudAdapter cloudAdapter = new CloudAdapter();

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult Index(UserViewModel user)
        {
            if (ModelState.IsValid && user.userID != null)
            {
                bool isValid = cloudAdapter.Authenticate(user.userID).Result;
                if (isValid)
                {
                    HttpContext.Session.Set<UserViewModel>("user", user);
                    return RedirectToAction("Index", "Home");
                }
            }
            return Index();
        }
    }
}
