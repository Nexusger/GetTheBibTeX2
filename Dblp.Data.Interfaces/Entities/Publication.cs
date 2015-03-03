using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Dblp.Data.Interfaces.Entities
{
    [DataContract]
    public class Publication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]

        [MaxLength(150)]
        public string Key { get; set; }
        [DataMember]
        public virtual AuthorList Authors { get; set; }
        [DataMember]
        [MaxLength(1000)]
        public string Title { get; set; }
        public virtual ConferenceEvent ConferenceEvent { get; set; }

        public Publication()
        {
            Authors = new AuthorList();
        }
    }
}

