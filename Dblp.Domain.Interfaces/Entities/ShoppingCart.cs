using System.Collections.Generic;
using System.Linq;

namespace Dblp.Domain.Interfaces.Entities
{
    public class ShoppingCart
    {
        private List<SearchResult> searchResults = new List<SearchResult>();

        public void AddItem(SearchResult searchResult)
        {
            if (searchResult == null)
                return;
            var item = searchResults.FirstOrDefault(s => s.Key == searchResult.Key);
            if (item == null)
                searchResults.Add(searchResult);
        }

        public void RemoveItem(SearchResult searchResult)
        {
            if (searchResult != null)
                searchResults.RemoveAll(i => i.Key == searchResult.Key);
        }

        public void Clear()
        {
            searchResults.Clear();
        }

        public IEnumerable<SearchResult> SearchResults
        {
            get { return searchResults; }
        }

    }
}