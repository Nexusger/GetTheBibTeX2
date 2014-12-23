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

        public IEnumerable<Person> People
        {
            get { return _context.Persons; }
            
        }
        public IEnumerable<Publication> Publications { get; private set; }
        public IEnumerable<Proceeding> Proceedings { get; private set; }
        public IEnumerable<SearchResult> SearchResults { get; private set; }
    }
}
