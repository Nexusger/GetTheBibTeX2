using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Temp.Entities
{
    public class Book
    {

        [Key]
        public string BookKey { get; set; }
        public DateTime? MDate { get; set; }
        public virtual ICollection<Person> Authors { get; set; }
        public string Title { get; set; }
        public string Pages { get; set; }
        public int Year { get; set; }
        public string ee { get; set; }
        public string Isbn { get; set; }
        public string Url { get; set; }
        public string Publisher { get; set; }

        public Book()
        {
            Authors = new List<Person>();
        }

        public Book(XElement element)
        {
            DateTime? dt = null;
            if (element.Attribute("mdate") != null)
            {
                dt = DateTime.Parse(element.Attribute("mdate").Value);
            }

            BookKey = element.Attribute("key").Value;
            MDate = dt;
            var xElement1 = element.Element("title");
            if (xElement1 != null) Title = xElement1.Value;
            var element1 = element.Element("pages");
            if (element1 != null) Pages = element1.Value;
            var xElement4 = element.Element("year");
            if (xElement4 != null) Year = int.Parse(xElement4.Value);
            var xElement3 = element.Element("ee");
            if (xElement3 != null) ee = xElement3.Value;
            var isbn = element.Element("isbn");
            if (isbn != null) Isbn = isbn.Value;
            var url = element.Element("url");
            if (url != null) Url = url.Value;
            var publisher = element.Element("publisher");
            if (publisher != null) Publisher = publisher.Value;
   
        }
    }
}




