using System;

namespace Dblp.Domain.Interfaces
{
    public interface IStatusRepository
    {
        long NumberOfConferences();
        long NumberOfConferenceEvents();
        long NumberOfAuthors();
        long NumberOfPublications();
        DateTime LastUpdated();
        LoadDetails DataLoadDetails();

    }
}
