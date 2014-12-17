using System.Runtime.Serialization;
using Dblp.WebUi.Models;

namespace Dblp.Domain.Entities
{
    [DataContract]
    public class SearchResult
    {
        public SearchResult(string key, string displayText, SearchResultSourceType searchResultSourceType)
        {
            Key = key;
            DisplayText = displayText;
            SearchResultSourceType = searchResultSourceType;
        }
        
        /// <summary>
        /// DBLP Key
        /// </summary>
        [DataMember]
        public string Key { get; private set; }
        
        [DataMember]
        public string DisplayText { get; private set; }
        
        
        [DataMember]
        public SearchResultSourceType SearchResultSourceType { get; private set; }
    }
}