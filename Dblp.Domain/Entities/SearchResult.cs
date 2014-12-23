using System.Runtime.Serialization;

namespace Dblp.Domain.Entities
{
    [DataContract]
    public class SearchResult
    {
        public SearchResult(string key, string displayText, long usage,SearchResultSourceType searchResultSourceType)
        {
            Key = key;
            DisplayText = displayText;
            SearchResultSourceType = searchResultSourceType;
            Usage = usage;
        }
        
        /// <summary>
        /// DBLP Key
        /// </summary>
        [DataMember]
        public string Key { get; private set; }
        
        [DataMember]
        public string DisplayText { get; private set; }

        public long Usage { get; set; }
        
        [DataMember]
        public SearchResultSourceType SearchResultSourceType { get; private set; }
    }
}