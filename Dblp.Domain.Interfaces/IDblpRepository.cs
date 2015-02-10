using System.Linq;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.Domain.Interfaces
{
    public interface IDblpRepository
    {
        Conference GetConferenceByKey(string conferenceKey);

        IQueryable<Conference> GetConferencesByName(string conferenceName);
        IQueryable<Conference> GetConferencesByAbbreviation(string conferenceAbbreviation);

        IQueryable<Conference> GetConferences(int maxAmount);
        IQueryable<Conference> GetConferences(string searchString);
        IQueryable<Conference> GetConferences(string searchString,int maxAmount);
        bool ConferenceExists(string key);

        IQueryable<SearchResult> GetSearchResults(int maxAmount);
        IQueryable<SearchResult> GetSearchResults(string searchString);
        IQueryable<SearchResult> GetSearchResults(string searchString,int maxAmount);


        SearchResult GetPublicationByKeyAsSearchResult(string publicationKey);
        IQueryable<Publication> GetPublications(int maxAmount);
        IQueryable<Publication> GetPublications(string searchString);
        IQueryable<Publication> GetPublications(string searchString, int maxAmount);
        bool PublicationExists(string key);
    }
}
