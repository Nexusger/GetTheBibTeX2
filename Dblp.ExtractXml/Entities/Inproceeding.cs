using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Temp.Abstract;
using Temp.Entities;

namespace Temp
{
    public class Inproceeding
    {
        [Key]
        public string InproceedingKey { get; set; }
        public DateTime? MDate { get; set; }
        public virtual List<Person> Authors { get; set; }
        public string Title { get; set; }
        public string Pages { get; set; }
        public string BookTitle { get; set; }
        public int Year { get; set; }
        public string ee { get; set; }
        public virtual Proceeding Proceeding { get; set; }

        public Inproceeding()
        {
            
        }
              public Inproceeding(XElement element)
        {
            DateTime? dt = null;
            if (element.Attribute("mdate") != null)
            {
                dt = DateTime.Parse(element.Attribute("mdate").Value);
            }

            InproceedingKey = element.Attribute("key").Value;
            MDate = dt;

            var xElement = element.Element("booktitle");
            if (xElement != null) BookTitle = xElement.Value;
            var element1 = element.Element("pages");
            if (element1 != null) Pages = element1.Value;
            var xElement2 = element.Element("title");
            if (xElement2 != null) Title = xElement2.Value;

            var xElement4 = element.Element("year");
            if (xElement4 != null) Year = int.Parse(xElement4.Value);


            var xElement3 = element.Element("ee");
            if (xElement3 != null) ee = xElement3.Value;

        }
    }

}
