using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Dblp.Domain.Abstract;
using Dblp.Domain.Entities;

namespace Dblp.Domain.Concrete
{
    public class XmlRepository : IDblpRepository
    {
        private XElement _repo;
        private IEnumerable<Person> _personRepo;
        private IEnumerable<Proceeding> _proceedingRepo;
        private List<SearchResult> _searchResultRepo;
        public XmlRepository()
        {
            _repo = XElement.Load(new StreamReader(@"D:\dblp\dblp-min.xml"));
            _searchResultRepo = new List<SearchResult>();
            //foreach (var person in People)
            //{
            //    foreach (var name in person.Names)
            //    {
            //        _searchResultRepo.Add(new SearchResult(person.Key, name, 0,SearchResultSourceType.Person,""));
            //    }
            //}
            foreach (var proceeding in Proceedings)
            {
                foreach (var name in proceeding.Editors)
                {
                    _searchResultRepo.Add(new SearchResult(proceeding.Key, name, 0, SearchResultSourceType.Paper,"Editor von "+proceeding.Title));
                }
                foreach (var name in proceeding.Authors)
                {
                    _searchResultRepo.Add(new SearchResult(proceeding.Key, name, 0, SearchResultSourceType.Paper, "Autor von "+proceeding.Title));
                }
                if (string.IsNullOrEmpty(proceeding.Series))
                {
                    _searchResultRepo.Add(new SearchResult(proceeding.Key, proceeding.Series, 0,
                        SearchResultSourceType.Paper, "Serie von "));}
                if (string.IsNullOrEmpty(proceeding.BookTitle))
                {
                    _searchResultRepo.Add(new SearchResult(proceeding.Key, proceeding.BookTitle, 0,
                        SearchResultSourceType.Paper, "Buchtitel von "));
                }
                if (string.IsNullOrEmpty(proceeding.Title)) 
                {
                    _searchResultRepo.Add(new SearchResult(proceeding.Key, proceeding.Title, 0,
                        SearchResultSourceType.Paper, ""));
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
        public IEnumerable<Publication> Publications { get; private set; }
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
    }
}
