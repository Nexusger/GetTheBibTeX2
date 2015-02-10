using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Dblp.Domain;
using Dblp.Domain.Interfaces;
using Dblp.Domain.Interfaces.Entities;

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
            return null;
        
        //    return _repository.SearchResults.Where(sr => sr.SearchResultSourceType != SearchResultSourceType.Person).Take(100);
        }
        [HttpGet]
        public IEnumerable<SearchResult> GetPeople()
        {
                return null;
                //return _repository.SearchResults.Where(sr => sr.SearchResultSourceType == SearchResultSourceType.Person).Take(100);
        }
        [HttpGet]
        public IEnumerable<SearchResult> GetConferences()
        {
            var searchResults = _repository.GetSearchResults(100);
            return
                searchResults;
        }
    }
}
