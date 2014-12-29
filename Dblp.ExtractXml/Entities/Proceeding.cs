using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Temp.Abstract;

namespace Temp.Entities
{
    public class Proceeding : IEntry
    {
        [Key]
        public string ProceedingKey { get; set; }
        public DateTime? MDate { get; set; }
        public virtual List<Person> Editors { get; set; }
        public string Title { get; set; }
        public long Volume { get; set; }
        public int Year { get; set; }
        public string Isbn { get; set; }
        public string BookTitle { get; set; }
        public string Publisher { get; set; }

        public Proceeding()
        {
            
        }
        public Proceeding(XElement element)
        {
            DateTime? dt = null;
            if (element.Attribute("mdate") != null)
            {
                dt = DateTime.Parse(element.Attribute("mdate").Value);
            }

            ProceedingKey = element.Attribute("key").Value;
            MDate = dt;

            var xElement = element.Element("booktitle");
            if (xElement != null) BookTitle = xElement.Value;
            var xElement1 = element.Element("isbn");
            if (xElement1 != null) Isbn = xElement1.Value;
            var element1 = element.Element("publisher");
            if (element1 != null) Publisher = element1.Value;
            var xElement2 = element.Element("title");
            if (xElement2 != null) Title = xElement2.Value;

            var element3 = element.Element("Volume");
            if (element3 != null) Volume = long.Parse(element3.Value);
            var xElement4 = element.Element("year");
            if (xElement4 != null) Year = int.Parse(xElement4.Value);

            var element2 = element.Element("BookTitle");
            if (element2 != null) BookTitle = element2.Value;
            var xElement3 = element.Element("Publisher");
            if (xElement3 != null) Publisher = xElement3.Value;
        }
    }

}

