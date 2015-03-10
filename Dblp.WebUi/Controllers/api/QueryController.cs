using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Dblp.Domain;
using Dblp.Domain.Interfaces;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.WebUi.Controllers.api
{
    /// <summary>
    /// Is used for the typeahead bloodhound search
    /// </summary>
    public class QueryController : ApiController
    {
        private IDblpRepository _repository;

        public QueryController(IDblpRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<SearchResult> GetConferences(string key)
        {
            var searchResults = _repository.GetConferencesAsSearchResults(key, 10);
            return searchResults;
        }
        [HttpGet]
        public IEnumerable<SearchResult> GetPublications(string key)
        {
            return _repository.GetPublicationsAsSearchResults(key, 10);
        }

        [HttpGet]
        public IEnumerable<SearchResult> GetAuthors(string key)
        {
            return _repository.GetAuthorsAsSearchResults(key, 10);
        }
    }
}
