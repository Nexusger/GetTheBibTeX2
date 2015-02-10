using System;
using System.Collections.Generic;

namespace Dblp.Domain.Interfaces.Entities
{
    public class Person
    {

        public string Key { get; set; }

        public DateTime Mdate { get; set; }
        public List<string> Names { get; set; }

        public List<string> Url { get; set; }

        public Dictionary<string,string> Notes { get; set; }
        
        public string CrossRef { get; set; }
        public string Cite { get; set; }
    }

}
