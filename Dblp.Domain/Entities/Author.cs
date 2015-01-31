using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Dblp.Domain.Entities
{
    [DataContract]
    public class Author
    {
        [Key]
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        public virtual Publication Publication { get; set; }
    }
}