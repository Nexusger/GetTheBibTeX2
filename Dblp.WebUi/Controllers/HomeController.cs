using System.Collections.Generic;
using System.Web.Mvc;
using Dblp.Domain.Entities;
using Dblp.WebUi.Models;

namespace Dblp.WebUi.Controllers
{
    public class HomeController : Controller
    {
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