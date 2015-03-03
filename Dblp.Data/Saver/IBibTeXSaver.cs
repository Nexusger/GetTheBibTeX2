using System.Collections.Generic;
using Dblp.Data.Interfaces.Entities;

namespace Dblp.Data.Saver
{
    public interface IBibTeXSaver
    {
        bool SaveBibTexEntries(IEnumerable<BibTexEntry> entries);
    }
}
