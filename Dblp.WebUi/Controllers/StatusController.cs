using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dblp.WebUi.Models;

namespace Dblp.WebUi.Controllers
{
    public class StatusController : Controller
    {
        // GET: Status
        public ActionResult Index()
        {
            var model = new StatusViewModel(new DateTime(2004, 6, 8), 15, 100);
            
            return View(model);
        }
    }
}