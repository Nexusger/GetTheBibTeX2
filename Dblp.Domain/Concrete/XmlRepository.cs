using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Xml.Linq;
using Dblp.Domain.Abstract;
using Dblp.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dblp.Domain.Concrete
{
    public class XmlRepository : IDblpRepository
    {
        private  XElement _repo;
        private IEnumerable<Person> _personRepo;
        private IEnumerable<Proceeding> _proceedingRepo;
        private List<SearchResult> _searchResultRepo;
        private  List<Conference> _structureRepo;
        private List<KeyValuePair<string, string>> _smallSearchResults;
        public XmlRepository()
        {
            _repo = XElement.Load(new StreamReader(@"D:\dblp\dblp-min.xml"));
            _structureRepo = JsonConvert.DeserializeObject<List<Conference>>(File.ReadAllText(@"D:\dblp\Structure.json"));
            _searchResultRepo = new List<SearchResult>();
            _smallSearchResults = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("conf/LAK/index","Test Text")
            };
            foreach (var conference in _structureRepo)
            {
                _searchResultRepo.Add(new SearchResult(conference.Key, conference.ConferenceTitle, 0, SearchResultSourceType.Conference, ""));
                foreach (var @event in conference.Events)
                {
                    _searchResultRepo.Add(new SearchResult(@event.Key, @event.Title, 0, SearchResultSourceType.Conference, ""));
                    if(@event.Publications!=null)
                    foreach (var publication in @event.Publications)
                    {
                        _searchResultRepo.Add(new SearchResult(publication.Key,publication.Title,0,SearchResultSourceType.Paper, ""));
                    }
                }
            }
        }

        public IEnumerable<Person> People
        {
            get
            {
                if (_personRepo == null)
                {
                    _personRepo = new List<Person>();
                    var people =
                        _repo.Nodes()
                            .Select(n => (n as XElement))
                            .Where(e => e.Name == "www" && e.Attribute("key").Value.StartsWith("homepages"));
                    _personRepo = people.Select(p => p.ToPerson());
                }
                return _personRepo;
            }
        }
        // public IEnumerable<Publication> Publications { get; private set; }
        public IEnumerable<Proceeding> Proceedings
        {
            get
            {
                if (_proceedingRepo == null)
                {
                    _proceedingRepo = new List<Proceeding>();
                    var people =
                        _repo.Nodes()
                            .Select(n => (n as XElement))
                            .Where(e => e.Name == "proceedings");
                    _proceedingRepo = people.Select(p => p.ToProceeding());
                }
                return _proceedingRepo;
            }
        }

        public IEnumerable<SearchResult> SearchResults
        {
            get
            {
                return _searchResultRepo;
            }
        }

        public IEnumerable<Conference> Conferences
        {
            get
            {
                return _structureRepo;//.Select(t=> { t.ConferenceTitle = _smallSearchResults.FirstOrDefault(k=>k.Key==t.Key).Value;return t;});
            }

        }
    }
}
