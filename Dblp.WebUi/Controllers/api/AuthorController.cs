using System.Linq;
using System.Web.Http;
using Dblp.Domain.Interfaces;
using Dblp.WebUi.Models;

namespace Dblp.WebUi.Controllers.api
{
    public  class AuthorController : ApiController
    {
        private IDblpRepository _repo;

        public AuthorController(IDblpRepository repo)
        {
            _repo = repo;
        }

        public AuthorSearchResult GetAuthorSearchResult(string key)
        {
            var publications = _repo.GetPublicationsForAuthorAsSearchResults(key);
            return new AuthorSearchResult(){AuthorName = key,Publications = publications.ToList()};
        }
    }
}