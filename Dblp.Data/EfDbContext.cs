using System.Data.Entity;
using Dblp.Data.Interfaces.Entities;

namespace Dblp.Data
{
    public class EfDbContext:DbContext
    {
        public EfDbContext():base("name=EfDbContext")
        {
        }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<ConferenceEvent> Events { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Author> Authors { get; set; }

    }
}
