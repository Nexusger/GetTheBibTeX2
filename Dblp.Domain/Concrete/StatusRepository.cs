using System;
using System.Linq;
using Dblp.Data.Interfaces;
using Dblp.Domain.Interfaces;

namespace Dblp.Domain.Concrete
{
    public class StatusRepository :IStatusRepository
    {
        private IDblpDataStore _store;

        public StatusRepository(IDblpDataStore store)
        {
            _store = store;
        }
        public long NumberOfConferences()
        {
            return _store.Conferences.Count();
        }

        public long NumberOfConferenceEvents()
        {
            return _store.ConferenceEvents.Count();
        }

        public long NumberOfAuthors()
        {
            return _store.Authors.Count();
        }

        public long NumberOfPublications()
        {
            return _store.Publications.Count();
        }

        public DateTime LastUpdated()
        {
            return DateTime.UtcNow;
        }

        public LoadDetails DataLoadDetails()
        {
            return new LoadDetails(){LastLoaded = DateTime.UtcNow,LoadTime = TimeSpan.FromMinutes(60),SizeOfXml = 99999};
        }
    }
}
