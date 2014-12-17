using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dblp.WebUi.Models;

namespace Dblp.WebUi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.CurrentSite = "Home";

            var x = new List<SearchResultViewModel>()
            {
                new SearchResultViewModel("homepages/d/TorbenDohrn", "Rome", "Eine toller Typ",
                    SearchResultSourceType.Person)
            };
            return View(x);
        }
        
    }
}