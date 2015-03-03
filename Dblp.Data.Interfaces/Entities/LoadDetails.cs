using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Dblp.Data.Interfaces.Entities
{
    [DataContract]
    public class LoadDetails
    {
        [Key]
        [DataMember]
        public long Key { get; set; }
        [DataMember]
        public TimeSpan LoadTime { get; set; }
        [DataMember]
        public DateTime LastLoaded { get; set; }

        [DataMember]
        public long AmountBhtFilesParsed { get; set; }
        [DataMember]
        public long SizeOfXml { get; set; }
    }
}
