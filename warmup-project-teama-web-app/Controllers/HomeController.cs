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

namespace warmup_project_teama_web_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ICloudAdapter cloudAdapter = new CloudAdapter();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Initial set-up of the view.
        /// </summary>
        /// <returns>View with empty table</returns>
        public IActionResult Index()
        {
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
            if (ModelState.IsValid && dataList.Count > 0)
            {
                TableViewModel entries = cloudAdapter.execute(dataList);
                return View(entries);
            }
            return View(new TableViewModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}