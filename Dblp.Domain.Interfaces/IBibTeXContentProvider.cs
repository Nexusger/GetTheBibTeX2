using System.Collections;
using System.Collections.Generic;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.Domain.Interfaces
{
    public interface IBibTeXContentProvider
    {
        byte[] GetBibTexFileBytes(IEnumerable<SearchResult> searchResults,BibTeXContentOptions options);
    }
}