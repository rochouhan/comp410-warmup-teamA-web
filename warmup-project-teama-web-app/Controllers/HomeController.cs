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

        /*
         * Initial view set-up
         */
        public IActionResult Index()
        {
            return View(new TableViewModel());
        }

        /*
         * Index view after an HTTP POST request.
         * Takes in parameters from the view.
         */
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
        public IActionResult Index(ICollection<KVPair> dataList)
        {
            if (ModelState.IsValid && dataList.Count > 0)
            {
                // This is all dummy code to make sure that the controller
                // successfully gets the data from the user-inputted query.
                // Change this so that it goes through the cloud adapter.
                List<Entry> dummyTable = new List<Entry>();
                foreach (var pair in dataList)
                {
                    dummyTable.Add(new Entry(pair.Key + ": " + pair.Value, DateTime.Now, new Dictionary<string, string>()));
                }

                return View(new TableViewModel(dummyTable));

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
