using System.Linq;
using Dblp.Data.Interfaces;
using Dblp.Domain.Interfaces;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.Domain.Concrete
{
    public class InMemoryRepository : IDblpRepository
    {
        public Conference GetConferenceByKey(string conferenceKey)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Conference> GetConferencesByName(string conferenceName)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Conference> GetConferencesByAbbreviation(string conferenceAbbreviation)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Conference> GetConferences(int maxAmount)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Conference> GetConferences(string searchString)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Conference> GetConferences(string searchString, int maxAmount)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<SearchResult> GetSearchResults(int maxAmount)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<SearchResult> GetSearchResults(string searchString)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<SearchResult> GetSearchResults(string searchString, int maxAmount)
        {
            throw new System.NotImplementedException();
        }

        public SearchResult GetPublicationByKeyAsSearchResult(string publicationKey)
        {
            throw new System.NotImplementedException();
        }

        public Publication GetPublicationByKey(string publicationKey)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Publication> GetPublications(int maxAmount)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Publication> GetPublications(string searchString)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Publication> GetPublications(string searchString, int maxAmount)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<SearchResult> GetPublicationsAsSearchResults(string searchString)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<SearchResult> GetPublicationsAsSearchResults(string searchString, int maxAmount)
        {
            throw new System.NotImplementedException();
        }

        public bool ConferenceExists(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool PublicationExists(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}
