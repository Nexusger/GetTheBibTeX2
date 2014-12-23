using System.Runtime.Serialization;

namespace Dblp.Domain.Entities
{
    [DataContract]
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

        
        
        /// <summary>
        /// DBLP Key
        /// </summary>
        [DataMember]
        public string Key { get; private set; }
        
        [DataMember]
        public string DisplayText { get; private set; }
        [DataMember]
        public long Usage { get; set; }
        
        [DataMember]
        public SearchResultSourceType SearchResultSourceType { get; private set; }
        [DataMember]
        public string Relation { get; private set; }
    }
}