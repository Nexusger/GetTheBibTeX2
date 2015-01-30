using System.Collections.Generic;
using Dblp.Domain.Entities;

namespace Dblp.ExtractBht.Interface
{
    public interface IConferenceStructurSaver
    {
        bool SaveConference(IEnumerable<Conference> conferences);
    }
}
