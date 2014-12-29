using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Dblp.Domain.Entities
{
    [DataContract]
    public class Publication
    {
        [Key]
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public List<string> Authors { get; set; }
        [DataMember]
        public string Title { get; set; }
    }
}
