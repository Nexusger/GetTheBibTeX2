using System.Collections.Generic;
using System.Linq;
using Dblp.Data.Interfaces.Entities;

namespace Dblp.Data.Interfaces
{
    public interface IDblpDataStore
    {
        //IEnumerable<SearchResult> SearchResults { get; }

        IQueryable<Conference> Conferences { get; }
        IQueryable<Publication> Publications { get; }
        IQueryable<Author> Authors { get; }
        IQueryable<ConferenceEvent> ConferenceEvents { get; }

    }


}