using System.Collections.Generic;
using System.Web.Http;
using Dblp.Domain.Interfaces;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.WebUi.Controllers.api
{
    public class ConferenceController : ApiController
    {
        private IDblpRepository _repo
            ;

        public ConferenceController(IDblpRepository repo)
        {
            _repo = repo;
        }

        //public IEnumerable<Conference> GetConferences()
        //{
        //    return _repo.GetConferences(15);
        //}

        public Conference GetConference(string key)
        {
            if (key == null)
            return null;

            var conference = _repo.GetConferenceByKey(key);
            return conference ?? null;
        }
    }
}
