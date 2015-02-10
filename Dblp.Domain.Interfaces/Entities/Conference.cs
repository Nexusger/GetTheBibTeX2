using System.Collections.Generic;

namespace Dblp.Domain.Interfaces.Entities
{
    public class Conference
    {
        public Conference()
        {
            
        }
        public Conference(string conferenceTitle, string key)
        {
            ConferenceTitle = conferenceTitle;
            Key = key;
            Events = new List<ConferenceEvent>();
        }

        public string Key { get; set; }
        public string Abbr { get; set; }
        public string ConferenceTitle { get; set; }

        public List<ConferenceEvent> Events { get; set; }

        public override string ToString()
        {
            return ConferenceTitle + "; Events: " + Events.Count;
        }
    }
}