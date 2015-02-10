using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.Domain
{
    public static class AuthorsHelper
    {
        public static List<Author> ToDomainAuthors(this Dblp.Data.Interfaces.Entities.AuthorList incommingAuthorList)
        {
            var outgoingAuthors = new List<Author>();
            foreach (var author in incommingAuthorList)
            {
                outgoingAuthors.Add(author.ToDomainAuthor());
            }
            return outgoingAuthors;
        }

        public static Author ToDomainAuthor(this Dblp.Data.Interfaces.Entities.Author incommingAuthor)
        {
            return new Author()
            {
                Name = incommingAuthor.Name
            };

        }
    }
}
