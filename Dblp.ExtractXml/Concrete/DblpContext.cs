using System.Data.Entity;
using Temp.Abstract;
using Temp.Entities;

namespace Temp.Concrete
{
    public class DblpContext : DbContext
    {
        public DblpContext()
            :base("name=DblpDatabase")
        {
            
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Proceeding> Proceedings { get; set; }
        public DbSet<Inproceeding> Inproceedings { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Inproceeding>().HasMany(t => t.Authors);

            modelBuilder.Entity<Inproceeding>().HasRequired(t => t.Proceeding);
            

        }
    }
}
