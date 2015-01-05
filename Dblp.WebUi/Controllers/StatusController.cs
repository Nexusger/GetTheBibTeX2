using System;
using System.Collections.Generic;
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
            var lastUpdated = DateTime.UtcNow;

            var statusElements = new List<StatusListElement>();
            var numberOfAuthors = _repo.People.Count();
            statusElements.Add( new StatusListElement()
            {
                DisplayText = "Anzahl Autoren",
                HoverText = "Mockdaten",
                SpecialMarker = true,
                Value = numberOfAuthors
            });
            var numberOfConferences = _repo.Conferences.Count();
            statusElements.Add(new StatusListElement()
            {
                DisplayText = "Anzahl Konferenzen",
                HoverText = "e.g. LAK, ACL, WASA...",
                SpecialMarker = false,
                Value = numberOfConferences
            });
            var numberOfEventsInConferences = _repo.Conferences.Sum(t=>t.SubConferences.Count);
            statusElements.Add(new StatusListElement()
            {
                DisplayText = "Anzahl Events",
                HoverText = "e.g. Proceedings of the 2nd International Workshop on Teaching Analytics, Leuven, Belgium, April 8, 2013 ",
                SpecialMarker = false,
                Value = numberOfEventsInConferences
            });
            var numberOfLinkedPublications = _repo.Conferences.Sum(t=>t.SubConferences.Where(k=>k.Publications!=null).Sum(b=>b.Publications.Count()));
            statusElements.Add(new StatusListElement()
            {
                DisplayText = "Anzahl verknüpfter Publikationen",
                HoverText = "e.g. Exploiting Popularity and Similarity for Link Recommendation in Twitter Networks",
                SpecialMarker = false,
                Value = numberOfLinkedPublications
            });
            var numberOfPublications = _repo.Proceedings.Count();
            statusElements.Add(new StatusListElement()
            {
                DisplayText = "Anzahl Publikationen",
                HoverText = "Mockdaten",
                SpecialMarker = true,
                Value = numberOfPublications
            });

            var model = new StatusViewModel(lastUpdated, statusElements);
            
            return View(model);
        }
    }
}