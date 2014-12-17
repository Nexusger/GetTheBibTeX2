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
        public XmlRepository()
        {
            _repo = XElement.Load(new StreamReader(@"D:\dblp\dblp-min.xml"));
        }

        public IEnumerable<Person> People
        {
            get
            {
                if (_personRepo == null)
                {
                    _personRepo=new List<Person>();
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
    }
}
