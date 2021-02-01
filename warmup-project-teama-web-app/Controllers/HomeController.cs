using System;
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
        /// Initial view set-up
        /// </summary>
        /// <returns>Initial view</returns>
        public IActionResult Index()
        {
            // ALL HARD-CODED INFO
            // PLEASE DELETE LATER
            Dictionary<string, string> otherinfo1 = new Dictionary<string, string>();
            otherinfo1.Add("height", "5.6");
            otherinfo1.Add("weight", "134");

            Dictionary<string, string> otherinfo2 = new Dictionary<string, string>();
            otherinfo2.Add("favorite food", "pasta");

            List<Entry> entries = new List<Entry>();
            entries.Add(new Entry("userid0000", DateTime.Now, otherinfo1));
            entries.Add(new Entry("userid2222", DateTime.UnixEpoch, otherinfo2));

            return View(new TableViewModel(entries));
        }

        /// <summary>
        /// Index view after an HTTP POST request.
        /// </summary>
        /// <param name="searchString">Parameters from the view to query the API</param>
        /// <param name="notUsed">Used to overload method</param>
        /// <returns>View with populated data from API</returns>
        [HttpPost]
        public IActionResult Index(string searchString, bool notUsed)
        {
            TableViewModel entries = cloudAdapter.execute(searchString);
            if (entries == null)
            {
                //TODO: Maybe this isn't the cleanest way to handle the error case
                return View(new TableViewModel(new List<Entry>()));
            }
            return View(entries);
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
