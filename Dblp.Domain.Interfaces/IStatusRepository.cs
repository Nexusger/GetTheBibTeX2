using System;

namespace Dblp.Domain.Interfaces
{
    public interface IStatusRepository
    {
        long NumberOfConferences();
        long NumberOfConferenceEvents();
        long NumberOfAuthors();
        long NumberOfPublications();
        LoadDetails DataLoadDetails();

    }
}
