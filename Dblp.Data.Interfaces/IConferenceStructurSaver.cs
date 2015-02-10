using System.Collections.Generic;
using Dblp.Data.Interfaces.Entities;

namespace Dblp.Data.Interfaces
{
    public interface IConferenceStructurSaver
    {
        bool SaveConferences(IEnumerable<Conference> conferences);
    }
}
