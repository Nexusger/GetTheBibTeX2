using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dblp.Domain.Interfaces;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.Domain
{
    public class BibTeXContentProvider:IBibTeXContentProvider
    {
        private readonly IDblpBibTexRepository _repository;

        public BibTeXContentProvider(IDblpBibTexRepository repository)
        {
            _repository = repository;
        }

        public byte[] GetBibTexFileBytes(IEnumerable<SearchResult> searchResults, BibTeXContentOptions options)
        {
            if (searchResults!= null && searchResults.Any())
            {
                var sb = new StringBuilder();
                foreach (var searchResult in searchResults)
                {
                    sb.AppendLine(_repository.GetBibTex(searchResult.Key));
                }
                var allEntries = sb.ToString();

                return Encoding.UTF8.GetBytes(allEntries);
            }
            return null;
        }
    }
}
