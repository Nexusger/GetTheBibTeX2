using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Dblp.Domain;
using Dblp.Domain.Interfaces;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.WebUi.Controllers.api
{
    public class QueryController : ApiController
    {
        private IDblpRepository _repository;

        public QueryController(IDblpRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<SearchResult> GetSearchResults(string key)
        {
            return _repository.GetConferences(key, 10).Select(t=>t.ToSearchResult());
        }

        [HttpGet]
        public IEnumerable<SearchResult> GetConferences(string key)
        {
            var searchResults = _repository.GetSearchResults(key, 10);
            return searchResults;
        }
    }
}
