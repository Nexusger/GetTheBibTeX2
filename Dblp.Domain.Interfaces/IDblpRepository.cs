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

        IQueryable<SearchResult> GetConferencesAsSearchResults(int maxAmount);
        IQueryable<SearchResult> GetConferencesAsSearchResults(string searchString);
        IQueryable<SearchResult> GetConferencesAsSearchResults(string searchString,int maxAmount);


        SearchResult GetPublicationByKeyAsSearchResult(string publicationKey);
        IQueryable<Publication> GetPublications(int maxAmount);
        IQueryable<Publication> GetPublications(string searchString);
        IQueryable<Publication> GetPublications(string searchString, int maxAmount);

        IQueryable<SearchResult> GetPublicationsAsSearchResults(int maxAmount);
        IQueryable<SearchResult> GetPublicationsAsSearchResults(string searchString);
        IQueryable<SearchResult> GetPublicationsAsSearchResults(string searchString, int maxAmount);
        IQueryable<SearchResult> GetPublicationsForAuthorAsSearchResults(string authorName);
        
        bool PublicationExists(string key);

        IQueryable<SearchResult> GetAuthorsAsSearchResults(int maxAmount);
        IQueryable<SearchResult> GetAuthorsAsSearchResults(string searchString);
        IQueryable<SearchResult> GetAuthorsAsSearchResults(string searchString, int maxAmount);
        
    }
}
