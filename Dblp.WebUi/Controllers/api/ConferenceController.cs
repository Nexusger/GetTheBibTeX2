using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Dblp.Domain.Abstract;
using Dblp.Domain.Entities;

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

        public IEnumerable<ConferenceStructure> GetConferences()
        {
            return _repo.Conferences.Take(15);
        }

        public ConferenceStructure GetConference(string key)
        {
            var conference = _repo.Conferences.Where(c => (c.Key != null && c.Key.Contains(key))).FirstOrDefault();
            //var conference = _repo.Conferences.Where(c => c.Key == key).FirstOrDefault();
            if (conference != null)
            {
                return conference;
            }
            //Check sub conferences
            conference = _repo.Conferences.Where(c => c.SubConferences.Where(t => t.Key == key).Any()).FirstOrDefault();
            if (conference != null)
            {
                return conference;
            }
            return null;
        }
    }
}
