using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dblp.Domain.Abstract;
using Dblp.Domain.Entities;

namespace Dblp.Domain.Concrete
{
    public class EfDblpRepository:IDblpRepository
    {
        private EfDbContext _context = new EfDbContext();

        public IEnumerable<Person> People { get; private set; }
        public IEnumerable<Proceeding> Proceedings { get; private set; }
        public IEnumerable<SearchResult> SearchResults { get; private set; }
        public IEnumerable<Conference> Conferences { get; private set; }
        public string GetTitleToKey(string key)
        {
            throw new NotImplementedException();
        }
    }
}
