using System.Runtime.Serialization;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.WebUi.Models
{
    [DataContract]
    public class SearchResultViewModel
    {
        public SearchResultViewModel(string key, string foundBy, string displayText, SearchResultSourceType searchResultSourceType)
        {
            Key = key;
            FoundBy = foundBy;
            DisplayText = displayText;
            SearchResultSourceType = searchResultSourceType;
        }
        [DataMember]
        public string Key { get; private set; }
        [DataMember]
        public string FoundBy { get; private set; }
        [DataMember]
        public string DisplayText { get; private set; }
        [DataMember]
        public SearchResultSourceType SearchResultSourceType { get; private set; }
    }
}