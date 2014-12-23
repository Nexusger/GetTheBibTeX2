using System.Collections.Generic;
using Dblp.Domain.Entities;

namespace Dblp.Domain.Abstract
{
    public interface IDblpRepository
    {
        IEnumerable<Person> People { get; }
        IEnumerable<Publication> Publications { get; }

        IEnumerable<Proceeding> Proceedings { get; }
        IEnumerable<SearchResult> SearchResults { get; }
    }
}