using System;

namespace Dblp.WebUi.Models
{
    public class StatusViewModel

    {
        public StatusViewModel(DateTime lastUpdated, long numberOfAuthors, long numberOfConferences)
        {
            LastUpdated = lastUpdated;
            NumberOfAuthors = numberOfAuthors;
            NumberOfConferences = numberOfConferences;
        }
        public DateTime LastUpdated { get; private set; }
        public long NumberOfAuthors { get; private set; }
        public long NumberOfConferences { get; set; }

    }
}