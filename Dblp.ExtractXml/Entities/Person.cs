using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Temp.Entities
{
    public class Person
    {
        public Person()
        {
            Notes = new List<string>();
            KnownNames = new List<string>();
        }
        public long Id { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
        public string Ee { get; set; }
        public ICollection<string> Notes { get; set; }
        public ICollection<string> KnownNames { get; set; }
        
    }
}
