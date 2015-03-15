using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Dblp.Data.Interfaces.Entities
{
    
    [DataContract]
    public class BibTexEntry
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [MaxLength(150)]
        public string Key { get; set; }

        [DataMember]
        public string Content { get; set; }

    }
}
