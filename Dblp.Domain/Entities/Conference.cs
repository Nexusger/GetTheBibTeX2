using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Dblp.Domain.Entities
{
    [DataContract]
    public class Conference
    {
        public Conference()
        {
            
        }
        public Conference(string conferenceTitle, string key)
        {
            ConferenceTitle = conferenceTitle;
            Key = key;
            Events = new EventList();
        }

        [Key]
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Abbr { get; set; }
        [DataMember]
        public string ConferenceTitle { get; set; }
        [DataMember]

        public EventList Events { get; set; }

        public override string ToString()
        {
            return ConferenceTitle + "; Events: " + Events.Count;
        }
    }
}