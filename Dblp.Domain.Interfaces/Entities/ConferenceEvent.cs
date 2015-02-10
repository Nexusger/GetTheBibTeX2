using System.Collections.Generic;

namespace Dblp.Domain.Interfaces.Entities
{
    public class ConferenceEvent
    {

        public string Key { get; set; }
        
        public string Title { get; set; }
        
        public List<Publication> Publications { get; set; }


        public override string ToString()
        {
            return Key + " " + Title;
        }
    }
}