using Dblp.Domain.Interfaces.Entities;

namespace Dblp.Domain
{
    public static class ConferenceHelper
    {
        public static Conference ToDomainConference(this Dblp.Data.Interfaces.Entities.Conference inCommingConference)
        {
            var outgoingConference = new Conference(inCommingConference.ConferenceTitle, inCommingConference.Key);
            outgoingConference.Abbr = inCommingConference.Abbr;
            outgoingConference.Events = inCommingConference.Events.ToDomainEvents();
            return outgoingConference;
        }

        public static SearchResult ToSearchResult(this Conference conference)
        {
            return new SearchResult(conference.Key,conference.ConferenceTitle,0,SearchResultSourceType.Conference,"");
        }
    }
}
