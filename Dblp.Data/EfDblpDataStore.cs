using System.Linq;
using Dblp.Data.Interfaces;
using Dblp.Data.Interfaces.Entities;

namespace Dblp.Data
{
    public class EfDblpDataStore:IDblpDataStore
    {
        private EfDbContext _context = new EfDbContext();

        public IQueryable<Conference> Conferences {
            get { return _context.Conferences; }  }

        public IQueryable<Publication> Publications
        {
            get { return _context.Publications; }
        }

        public IQueryable<Author> Authors
        {
            get { return _context.Authors; }
        }

        public IQueryable<ConferenceEvent> ConferenceEvents
        {
            get { return _context.Events; }
        }

        public IQueryable<LoadDetails> LoadDetails
        {
            get { return _context.LoadDetails; }
        }
    }
}
