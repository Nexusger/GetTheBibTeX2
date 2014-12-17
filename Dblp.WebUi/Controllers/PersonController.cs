using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dblp.Domain.Abstract;

namespace Dblp.WebUi.Controllers
{
    public class PersonController : Controller
    {
        private IDblpRepository _repository;

        private const int Pagesize = 2;

        public PersonController(IDblpRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List(int page = 1)
        {
            //return View(_repository.Persons.OrderBy(p=>p.Id).Skip(Pagesize*(page-1)).Take(Pagesize));
            return View(_repository.People.ToList());
        }
    }
}