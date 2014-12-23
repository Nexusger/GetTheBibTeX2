using System;

namespace Dblp.WebUi.Models
{
    public class StatusViewModel

    {
        public StatusViewModel(DateTime lastUpdated, long numberOfAuthors, long numberOfConferences, long numberOfPublications)
        {
            LastUpdated = lastUpdated;
            NumberOfAuthors = numberOfAuthors;
            NumberOfConferences = numberOfConferences;
            NumberOfPublications = numberOfPublications;
        }
        public DateTime LastUpdated { get; private set; }
        public long NumberOfAuthors { get; private set; }
        public long NumberOfConferences { get; private set; }

        public long NumberOfPublications { get; private set; }

    }
}