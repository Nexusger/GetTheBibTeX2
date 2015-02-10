namespace Dblp.Domain.Interfaces.Entities
{
    public class SearchResult
    {
        public SearchResult(string key, string displayText, long usage, SearchResultSourceType searchResultSourceType, string relation)
        {
            Key = key;
            DisplayText = displayText;
            Usage = usage;
            SearchResultSourceType = searchResultSourceType;
            Relation = relation;
        }

        public SearchResult()
        {
            
        }


        /// <summary>
        /// DBLP Key
        /// </summary>
        public string Key { get; set; }

        public string DisplayText { get; set; }
        public long Usage { get; set; }

        public SearchResultSourceType SearchResultSourceType { get; set; }
        public string Relation { get; set; }
    }
}