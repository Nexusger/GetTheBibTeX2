using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.Domain
{
    public static class EventHelper
    {
        public static List<ConferenceEvent> ToDomainEvents(
            this Dblp.Data.Interfaces.Entities.EventList incommingEventList)
        {
            var outgoingEvents = new List<ConferenceEvent>();
            if(incommingEventList!=null)
                outgoingEvents.AddRange(incommingEventList.Where(k => k != null).Select(conferenceEvent => conferenceEvent.ToDomainConferenceEvent()).ToList());
            return outgoingEvents;
        }

        public static ConferenceEvent ToDomainConferenceEvent(this Dblp.Data.Interfaces.Entities.ConferenceEvent incommingConferenceEvent)
        {
            var outgoingConferenceEvent = new ConferenceEvent();
            outgoingConferenceEvent.Key = incommingConferenceEvent.Key;
            outgoingConferenceEvent.Title = incommingConferenceEvent.Title;
            outgoingConferenceEvent.Publications = incommingConferenceEvent.Publications.ToDomainPublications();
            return outgoingConferenceEvent;
        }
    }
}
