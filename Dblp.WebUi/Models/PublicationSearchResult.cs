using Dblp.Domain.Interfaces.Entities;

namespace Dblp.WebUi.Models
{
    public class PublicationSearchResult
    {
        public string PublicationName { get; set; }
        public SearchResult Publication{ get; set; }
    }
}