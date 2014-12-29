using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Dblp.Domain.Entities
{
    [DataContract]
    public class SubConference
    {
        [Key]
        [DataMember]
        public string Key { get; set; }
        
        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public List<Publication> Publications { get; set; }
        public override string ToString()
        {
            return Key + " " + Title;
        }
    }
}