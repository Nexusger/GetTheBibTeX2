using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Dblp.Data.Interfaces.Entities
{
    [DataContract]
    public class ConferenceEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember(Name="EventKey")]
        [MaxLength(150)]
        public string EventKey { get; set; }
        
        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public virtual List<Publication> Publications { get; set; }

        public virtual Conference Confercence { get; set; }

        public override string ToString()
        {
            return EventKey + " " + Title;
        }
    }
}