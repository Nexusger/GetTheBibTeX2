using System.Collections.Generic;

namespace Dblp.Domain.Interfaces.Entities
{
    public class Publication
    {
        public Publication()
        {
            Authors = new List<Author>();
        }
        public string Key { get; set; }
        public List<Author> Authors{ get; set; }
        public string Title { get; set; }
        
    }
}

