using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Dblp.Domain.Abstract;
using Dblp.Domain.Entities;

namespace Dblp.WebUi.Controllers
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
        public IEnumerable<SearchResult> GetSearchResults(string id)
        {
            if (id.Equals("person"))
            {
                return _repository.SearchResults.Where(sr => sr.SearchResultSourceType == SearchResultSourceType.Person).Take(100);
            }
            return _repository.SearchResults.Where(sr => sr.SearchResultSourceType != SearchResultSourceType.Person).Take(100);
        }
    }
}
