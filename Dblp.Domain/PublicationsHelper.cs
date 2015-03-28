using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.Domain
{
    public static class PublicationsHelper
    {
        public static List<Publication> ToDomainPublications(
            this List<Dblp.Data.Interfaces.Entities.Publication> incommingPublications)
        {
            var outgoingPublications = new List<Publication>();
            foreach (var incommingPublication in incommingPublications)
            {
                outgoingPublications.Add(incommingPublication.ToDomainPublication());
            }
            return outgoingPublications;
        }

        public static Publication ToDomainPublication(
            this Dblp.Data.Interfaces.Entities.Publication incommingPublication)
        {
            var outgoingPublication = new Publication();
            outgoingPublication.Key = incommingPublication.Key;
            outgoingPublication.Title = incommingPublication.Title;
            outgoingPublication.Authors = incommingPublication.Authors.ToDomainAuthors();
            return outgoingPublication;
        }
    }
}
