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
    public class PrefetchController : ApiController
    {
        private IDblpRepository _repository;

        public PrefetchController(IDblpRepository repository)
        {
            _repository = repository;
        }

        
        [HttpGet]
        public IEnumerable<SearchResult> GetPublications()
        {
            return _repository.GetPublicationsAsSearchResults(100);
        }

        [HttpGet]
        public IEnumerable<SearchResult> GetAuthors()
        {
            return _repository.GetAuthorsAsSearchResults(100);
        }

        [HttpGet]
        public IEnumerable<SearchResult> GetConferences()
        {
            var searchResults = _repository.GetConferencesAsSearchResults(100);
            return
                searchResults;
        }
    
    }
}
