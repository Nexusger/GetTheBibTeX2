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
            return View();
        }
    }
}