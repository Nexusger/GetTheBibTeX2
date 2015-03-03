using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Dblp.Data.Interfaces.Entities;

namespace Dblp.Data
{
    public class EfDbContext : DbContext
    {
        public EfDbContext()
            : base("name=EfDbContext")
        {
        }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<ConferenceEvent> Events { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BibTexEntry> BibTexEntries { get; set; }

        public DbSet<LoadDetails> LoadDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publication>()
         .Property(x => x.Key)
         .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_Publication_Key")));
            modelBuilder.Entity<Publication>()
         .Property(x => x.Title)
         .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_Publication_Title")));
            modelBuilder.Entity<Author>()
         .Property(x => x.Name)
         .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_Author_Name")));
            modelBuilder.Entity<Conference>()
      .Property(x => x.ConferenceTitle)
      .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_Conference_ConferenceTitle")));
            modelBuilder.Entity<Conference>()
      .Property(x => x.Abbr)
      .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_Conference_Abbr")));
            modelBuilder.Entity<BibTexEntry>()
.Property(x => x.Key)
.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_BibTexEntry_Key")));
        }

    }
}
