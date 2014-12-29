using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Dblp.Domain.Entities
{
    [DataContract]
    public class ConferenceStructure
    {
        [Key]
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        private string ConferenceTitle;
        [DataMember]
        public InternalSubConferenceList SubConferences { get; set; }
        public ConferenceStructure(string conferenceTitle, string key)
        {
            ConferenceTitle = conferenceTitle;
            Key = key;
            SubConferences = new InternalSubConferenceList();
        }

        public override string ToString()
        {
            return ConferenceTitle + " " + SubConferences.Count;
        }
    }
    [CollectionDataContract]
    public class InternalSubConferenceList : List<SubConference> { }
}