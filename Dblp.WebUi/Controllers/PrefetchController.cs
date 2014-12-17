using System.Collections.Generic;
using System.Web.Http;
using Dblp.Domain.Entities;
using Dblp.WebUi.Models;

namespace Dblp.WebUi.Controllers
{
    public class PrefetchController : ApiController
    {
        // GET: api/Prefetch
        public IEnumerable<SearchResultViewModel> Get()
        {
            var mockResult = new List<SearchResultViewModel>()
            {
                new SearchResultViewModel("homepages/d/TorbenDohrn", "none", "Torben Dohrn",
                    SearchResultSourceType.Person),
                    new SearchResultViewModel("homepages/d/FabianDohrn", "none", "Fabian Dohrn",
                    SearchResultSourceType.Person),
                    new SearchResultViewModel("conf/l/lak", "none", "Learning Analyics Konference",
                    SearchResultSourceType.Conference)
            };

            return mockResult;
        }

        // GET: api/Prefetch/5
        public SearchResultViewModel Get(int id)
        {
            return new SearchResultViewModel("homepages/d/TorbenDohrn", "none", "Torben Dohrn",
                SearchResultSourceType.Person);
        }
    }
}
