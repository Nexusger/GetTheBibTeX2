using System;
using System.Linq;
using System.Web.Mvc;
using Dblp.Domain.Abstract;
using Dblp.WebUi.Models;

namespace Dblp.WebUi.Controllers
{
    public class StatusController : Controller
    {

        private IDblpRepository _repo;

        public StatusController(IDblpRepository repo)
        {
            _repo = repo;
        }
        // GET: Status
        public ActionResult Index()
        {

            var numberOfAuthors = _repo.People.Count();
            var numberOfPublications = _repo.Proceedings.Count();


            var model = new StatusViewModel(DateTime.UtcNow, numberOfAuthors, 0,numberOfPublications);
            
            return View(model);
        }
    }
}