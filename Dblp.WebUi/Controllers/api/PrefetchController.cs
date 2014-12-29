using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Dblp.Domain.Abstract;
using Dblp.Domain.Entities;

namespace Dblp.WebUi.Controllers.api
{
    public class PrefetchController : ApiController
    {
        private IDblpRepository _repository;

        public PrefetchController(IDblpRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Prefetch
        [HttpGet]
        public IEnumerable<SearchResult> GetSearchResults()
        {
            return _repository.SearchResults.Where(sr => sr.SearchResultSourceType != SearchResultSourceType.Person).Take(100);
        }
        [HttpGet]
        public IEnumerable<SearchResult> GetPeople()
        {
                return _repository.SearchResults.Where(sr => sr.SearchResultSourceType == SearchResultSourceType.Person).Take(100);
        }
        [HttpGet]
        public IEnumerable<SearchResult> GetConferences()
        {
            return
                _repository.Conferences.Take(100)
                    .Select(t => (new SearchResult(t.Key, "", 0, SearchResultSourceType.Conference, "")));
        }
    }
}
