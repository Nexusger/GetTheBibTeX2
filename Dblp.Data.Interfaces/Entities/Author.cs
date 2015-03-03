using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Dblp.Data.Interfaces.Entities
{
    [DataContract]
    public class Author
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [DataMember]
        [MaxLength(1000)]
        public string Name { get; set; }
        
        public virtual Publication Publication { get; set; }
    }
}