using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dblp.Domain.Entities
{
    public class Proceeding
    {
        public DateTime MDate { get; set; }
        public string Key { get; set; }
        public List<string> Editors { get; set; }
        public string Title { get; set; }
        public string BookTitle { get; set; }
        public string Publisher { get; set; }
        public long Year { get;  set; }
        public string UrlOnDblp { get; set; }
        public string Isbn { get; set; }
        public List<string> Ee { get;  set; }
        public string Volume { get; set; }
        public string Series { get; set; }
        public string Pages { get; set; }
        public List<string> Authors { get; set; }
        public string Number { get; set; }
        public Dictionary<string,string> Notes { get; set; }
        public string Journal { get; set; }
        public string Address { get; set; }
        public string CrossRef { get; set; }
        public string Cite { get; set; }


    }
}
