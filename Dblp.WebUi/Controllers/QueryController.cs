using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Dblp.Domain.Abstract;
using Dblp.Domain.Entities;

namespace Dblp.WebUi.Controllers
{
    public class QueryController : ApiController
    {
        private IDblpRepository _repository;

        public QueryController(IDblpRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<SearchResult> GetSearchResults(string query)
        {
           return _repository.SearchResults.Where(sr => (sr.DisplayText!=null && sr.DisplayText.Contains(query))).Take(10);
        }
    }
}
