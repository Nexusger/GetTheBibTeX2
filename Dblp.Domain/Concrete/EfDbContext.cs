using System.Data.Entity;
using Dblp.Domain.Entities;

namespace Dblp.Domain.Concrete
{
    public class EfDbContext:DbContext
    {
        public EfDbContext():base("name=EfDbContext")
        {
        }
        public DbSet<Person> Persons { get; set; }

    }
}
