using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Dblp.Domain.Entities
{
    [DataContract]
    public class Publication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public AuthorList Authors { get; set; }
        [DataMember]
        public string Title { get; set; }
        public virtual ConferenceEvent ConferenceEvent { get; set; }
    }
}

