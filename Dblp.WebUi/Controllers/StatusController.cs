using System.Collections.Generic;
using System.Web.Mvc;
using Dblp.Domain.Interfaces;
using Dblp.WebUi.Models;

namespace Dblp.WebUi.Controllers
{
    public class StatusController : Controller
    {

        private IStatusRepository _repo;

        public StatusController(IStatusRepository repo)
        {
            _repo = repo;
        }
        // GET: Status
        public ActionResult Index()
        {

            var statusElements = new List<StatusListElement>();
            var numberOfAuthors = _repo.NumberOfAuthors();
            statusElements.Add(new StatusListElement()
            {
                DisplayText = "Anzahl Autoren",
                HoverText = "e.g. Albrecht Fortenbacher",
                SpecialMarker = false,
                Value = numberOfAuthors
            });
            var numberOfConferences = _repo.NumberOfConferences();
            statusElements.Add(new StatusListElement()
            {
                DisplayText = "Anzahl Konferenzen",
                HoverText = "e.g. LAK, ACL, WASA...",
                SpecialMarker = false,
                Value = numberOfConferences
            });
            var numberOfConferenceEvents = _repo.NumberOfConferenceEvents();
            statusElements.Add(new StatusListElement()
            {
                DisplayText = "Anzahl Events",
                HoverText = "e.g. Proceedings of the 2nd International Workshop on Teaching Analytics, Leuven, Belgium, April 8, 2013 ",
                SpecialMarker = false,
                Value = numberOfConferenceEvents
            });
            var numberOfPublications = _repo.NumberOfPublications();
            statusElements.Add(new StatusListElement()
            {
                DisplayText = "Anzahl Publikationen",
                HoverText = "Mockdaten",
                SpecialMarker = false,
                Value = numberOfPublications
            });


            var loadDetails = _repo.DataLoadDetails();

            var model = new StatusViewModel(loadDetails, statusElements);

            return View(model);
        }
    }
}