using System.Web.Http;
using Dblp.Domain.Interfaces;
using Dblp.WebUi.Models;

namespace Dblp.WebUi.Controllers.api
{
    public class PublicationController : ApiController
    {
        private IDblpRepository _repo;

        public PublicationController(IDblpRepository repo)
        {
            _repo = repo;
        }

        public PublicationSearchResult GetPublicationSearchResult(string key)
        {
            var publication = _repo.GetPublicationByKeyAsSearchResult(key);
            return new PublicationSearchResult(){PublicationName = publication.DisplayText,Publication  = publication};
        }
    }
}
