using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dblp.Data.Interfaces;
using Dblp.Domain.Interfaces;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.Domain.Concrete
{
    class EfRepository : IDblpRepository
    {
        private IDblpDataStore _store;
        private readonly int _maxAllowedConferences;
        private Expression<Func<Conference, SearchResult>> _newSearchResult;
        public EfRepository(IDblpDataStore store)
        {
            _store = store;
            _maxAllowedConferences = 10;
            _newSearchResult = conference => new SearchResult()
            {
                DisplayText = conference.ConferenceTitle,
                Key = conference.Key,
                Relation = "",
                SearchResultSourceType = SearchResultSourceType.Conference,
                Usage = 0
            };
        }

        
        public Conference GetConferenceByKey(string conferenceKey)
        {
            var firstOrDefault = _store.Conferences.FirstOrDefault(t => t.Key == conferenceKey);
            if (firstOrDefault!=null)
            return firstOrDefault.ToDomainConference();
            return null;
        }

        public IQueryable<Conference> GetConferencesByName(string conferenceName)
        {
            var conferencesByName = _store.Conferences.Where(t => t.ConferenceTitle.Contains(conferenceName)).ToList();
            return conferencesByName.Select(k => k.ToDomainConference()).AsQueryable();
        }

        public IQueryable<Conference> GetConferencesByAbbreviation(string conferenceAbbreviation)
        {
            var conferencesByName = _store.Conferences.Where(t => t.Abbr.Contains(conferenceAbbreviation)).ToList();
            return conferencesByName.Select(k => k.ToDomainConference()).AsQueryable();
        }

        public IQueryable<Conference> GetConferences(int maxAmount)
        {
            if (maxAmount > _maxAllowedConferences)
                maxAmount = _maxAllowedConferences;
            return _store.Conferences.Take(maxAmount).ToList().Select(t => t.ToDomainConference()).AsQueryable();
        }

        public IQueryable<Conference> GetConferences(string searchString)
        {
            
            return GetConferences(searchString, _maxAllowedConferences);
        }

        public IQueryable<Conference> GetConferences(string searchString, int maxAmount)
        {
            return
                _store.Conferences.Where(
                    t => (t.Abbr.Contains(searchString)) || (t.ConferenceTitle.Contains(searchString)))
                    .Take(maxAmount)
                    .ToList()
                    .Select(k => k.ToDomainConference())
                    .AsQueryable();
        }


        public SearchResult GetConferenceEventsByKeyAsSearchResult(string conferenceEventKey)
        {
            return _store.ConferenceEvents.Where(t => t.EventKey == conferenceEventKey).Select(k=> (new SearchResult() { Key = k.EventKey, DisplayText = k.Title + " (" + k.Publications.Count + " Publikationen )", Usage = 0, SearchResultSourceType = SearchResultSourceType.Event, Relation = "" })).FirstOrDefault();
        }

        public IQueryable<SearchResult> GetConferencesAsSearchResults(int maxAmount)
        {
            return _store.Conferences.Take(maxAmount).Select(t => (new SearchResult() { Key = t.Key, DisplayText = t.ConferenceTitle + " (" + t.Abbr + ")", Usage = 0, SearchResultSourceType = SearchResultSourceType.Conference, Relation = "" }));
        }

        public IQueryable<SearchResult> GetConferencesAsSearchResults(string searchString)
        {
            return GetConferencesAsSearchResults(searchString, _maxAllowedConferences);
        }

        public IQueryable<SearchResult> GetConferencesAsSearchResults(string searchString, int maxAmount)
        {
            return _store.Conferences.Where(
                t => (t.Abbr.Contains(searchString)) || (t.ConferenceTitle.Contains(searchString)))
                .Select(k => (new SearchResult() { Key = k.Key, DisplayText = k.ConferenceTitle+" ("+k.Abbr+")", Usage = 0, SearchResultSourceType = SearchResultSourceType.Conference, Relation = "" }));
        }

        public SearchResult GetPublicationByKeyAsSearchResult(string publicationKey)
        {
            var publication = _store.Publications.FirstOrDefault(t => t.Key == publicationKey);
            if (publicationKey != null)
            {
                return new SearchResult(publication.Key,publication.Title,0,SearchResultSourceType.Paper, "");
            }
            return null;
        }

        public IQueryable<Publication> GetPublications(int maxAmount)
        {
            var publications = _store.Publications.Take(maxAmount).Select(t=>t.ToDomainPublication());
            return publications;
        }

        public IQueryable<Publication> GetPublications(string searchString)
        {
            var publications = _store.Publications.Where(k=>k.Title.Contains(searchString)).Select(t => t.ToDomainPublication());
            return publications;
        }

        public IQueryable<Publication> GetPublications(string searchString, int maxAmount)
        {
            var publications = _store.Publications.Where(k => k.Title.Contains(searchString)).Take(maxAmount).Select(t => t.ToDomainPublication());
            return publications;
        }

        public IQueryable<SearchResult> GetPublicationsAsSearchResults(int maxAmount)
        {
            var publication = _store.Publications.Take(maxAmount);
            if (publication.Any())
            {
                return publication.Select(t => new SearchResult() { Key = t.Key, DisplayText = t.Title, Usage = 0, SearchResultSourceType = SearchResultSourceType.Paper, Relation = "" });
            }
            return new List<SearchResult>().AsQueryable();
        }

        public IQueryable<SearchResult> GetPublicationsAsSearchResults(string searchString)
        {
            return GetPublicationsAsSearchResults(searchString, _maxAllowedConferences);
        }

        public IQueryable<SearchResult> GetPublicationsAsSearchResults(string searchString, int maxAmount)
        {
            var publication = _store.Publications.Where(t => t.Title.Contains(searchString)).Take(maxAmount);
            if (publication.Any())
            {
                return publication.Select(t => new SearchResult(){Key = t.Key, DisplayText = t.Title, Usage = 0, SearchResultSourceType = SearchResultSourceType.Paper, Relation = ""});
            }
            return new List<SearchResult>().AsQueryable();
        }

        public IQueryable<SearchResult> GetPublicationsForAuthorAsSearchResults(string authorName)
        {
            var authors = _store.Authors.Where(t => t.Name == authorName);
            var publication = _store.Publications.Where(t=> authors.Any(k=>k.Publication==t));
            if (publication.Any())
            {
                return publication.Select(t => new SearchResult() { Key = t.Key, DisplayText = t.Title, Usage = 0, SearchResultSourceType = SearchResultSourceType.Paper, Relation = "" });
            }
            return new List<SearchResult>().AsQueryable();
        }

        public IQueryable<SearchResult> GetPublicationsForEventsAsSearchResults(string eventKey)
        {
            var conferenceevent = _store.ConferenceEvents.FirstOrDefault(t => t.EventKey == eventKey);
            return conferenceevent.Publications.Select(t => (new SearchResult()
            {
                Key = t.Key,
                DisplayText = t.Title,
                SearchResultSourceType = SearchResultSourceType.Paper,
                Relation = "",
                Usage = 0
            })).AsQueryable();
        }

        public bool ConferenceExists(string key)
        {
            return _store.Conferences.Any(t => t.Key == key);
        }

        public bool PublicationExists(string key)
        {
            return _store.Publications.Any(t => t.Key == key);
        }

        public IQueryable<SearchResult> GetAuthorsAsSearchResults(int maxAmount)
        {
            var authors = _store.Authors.Select(t => t.Name).Distinct().Take(maxAmount);
            if (authors.Any())
            {
                return authors.Select(t => new SearchResult() { Key = "", DisplayText = t, Usage = 0, SearchResultSourceType = SearchResultSourceType.Person, Relation = "" });
            }
            return new List<SearchResult>().AsQueryable();
        }

        public IQueryable<SearchResult> GetAuthorsAsSearchResults(string searchString)
        {
            var authors = _store.Authors.Select(t => t.Name).Distinct().Where(t => t.Contains(searchString));
            if (authors.Any())
            {
                return authors.Select(t => new SearchResult() { Key = "", DisplayText = t, Usage = 0, SearchResultSourceType = SearchResultSourceType.Person, Relation = "" });
            }
            return new List<SearchResult>().AsQueryable();
        }

        public IQueryable<SearchResult> GetAuthorsAsSearchResults(string searchString, int maxAmount)
        {
            var authors = _store.Authors.Select(t => t.Name).Distinct().Where(t => t.Contains(searchString)).Take(maxAmount);
            if (authors.Any())
            {
                return authors.Select(t => new SearchResult() { Key="", DisplayText = t, Usage = 0, SearchResultSourceType = SearchResultSourceType.Person, Relation = "" });
            }
            return new List<SearchResult>().AsQueryable();
        }
    }
}
