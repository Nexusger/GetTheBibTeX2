using System.Linq;
using System.Web.Mvc;
using Dblp.Domain.Abstract;

namespace Dblp.WebUi.Controllers
{
    public class ProceedingController : Controller
    {
        private IDblpRepository _repository;

        public ProceedingController(IDblpRepository repository)
        {
            _repository = repository;
        }
        

        // GET: Proceeding
        public ActionResult Index()
        {
            return View(_repository.Proceedings.ToList());
        }
    }
}