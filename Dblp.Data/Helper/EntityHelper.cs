using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Dblp.Data;

namespace Dblp.Data.Helper
{
    public static class EntityHelper
    {
        //public static Person ToPerson(this XElement element)
        //{
        //    var url = element.ExtractUrl();
        //    var names = element.ExtractAuthors();
        //    var notes = element.ExtractDictionary("note","type");
        //    var mdate = element.ExtractDateFromAttribute("mdate");
        //    var key = element.ExtractDblpKey();
        //    return new Person()
        //    {
        //        Key= key,
        //        Mdate = mdate,
        //        Names = names,
        //        Url = url,
        //        Notes =notes
        //    };
        //}

        //public static Proceeding ToProceeding(this XElement element)
        //{
        //    var editors = element.ExtractEditors();
        //    var notes = element.ExtractDictionary("note", "type");
        //    var mdate = element.ExtractDateFromAttribute("mdate");
        //    var key = element.ExtractDblpKey();
        //    var authors = element.ExtractAuthors();
        //    var title = element.ExtractNode("title");
        //    var publisher = element.ExtractNode("publisher");
        //    var year = element.ExtractNode("year");
        //    var isbn = element.ExtractNode("isbn");
        //    var volume = element.ExtractNode("volume");
        //    var urlOnDblp = element.ExtractNode("url");
        //    var pages = element.ExtractNode("pages");
        //    var series = element.ExtractNode("series");
        //    var number = element.ExtractNode("number");
        //    var ee = element.ExtractMultipleNodes("ee");
        //    var journal = element.ExtractNode("journal");
        //    var address = element.ExtractNode("address");
        //    var crossRef = element.ExtractNode("crossref");
        //    var cite = element.ExtractNode("cite");
        //    return new Proceeding()
        //    {
        //        MDate=mdate,
        //        Key = key,
        //        Editors = editors,
        //        Title = title,
        //        BookTitle = title,
        //        Publisher = publisher,
        //        Year = Int32.Parse(year),
        //        UrlOnDblp = urlOnDblp,
        //        Isbn = isbn,
        //        Ee=ee,
        //        Volume=volume,
        //        Series = series,
        //        Pages=pages,
        //        Authors=authors,
        //        Number=number,
        //        Notes=notes,
        //        Journal=journal,
        //        Address=address,
        //        CrossRef = crossRef,
        //        Cite=cite,
                
        //    };
        //}


        private static string ExtractDblpKey(this XElement element)
        {
            return element.ExtractAttribute("key");
        }
        private static List<string> ExtractAuthors(this XElement element)
        {
            return element.ExtractMultipleNodes("author");
        }
        private static List<string> ExtractEditors(this XElement element)
        {
            return element.ExtractMultipleNodes("editor");
        }
        private static List<string> ExtractUrl(this XElement element)
        {
            return element.ExtractMultipleNodes("url");
        }



        
        
        private static string ExtractAttribute(this XElement element,string attributeName)
        {
            return element.Attribute(attributeName).Value;
        }
        private static string ExtractNode(this XElement element, string nodeName)
        {
            var node = element.Nodes().Select(n => (n as XElement)).FirstOrDefault(e => e.Name == nodeName);
            if (node != null)
                return node.Value;
            return null;
        }
        private static DateTime ExtractDateFromAttribute(this XElement element, string attributeName)
        {
            return DateTime.Parse(element.ExtractAttribute(attributeName));
        }

        private static List<string> ExtractMultipleNodes(this XElement element,string nodeName)
        {
            var nodeValues = new List<string>();
            if (element.Nodes().Select(n => (n as XElement)).Any(e => e.Name == nodeName))
            {
                nodeValues.AddRange(element.Nodes().Select(n => (n as XElement)).Where(e => e.Name == nodeName).Select(u => u.Value));
            }
            return nodeValues;
        }

        

        private static Dictionary<string, string> ExtractDictionary(this XElement element, string nodeName, string attributeName)
        {
            var notes = new Dictionary<string, string>();
            if (element.Nodes().Select(n => (n as XElement)).Any(e => e.Name == nodeName))
            {
                var count = 0;
                foreach (var e in (element.Nodes().Select(n => (n as XElement)).Where(e => e.Name == nodeName)))
                {

                    if (e.Attribute(attributeName) != null)
                    {
                        notes.Add(e.Attribute(attributeName).Value, e.Value);
                    }
                    else
                    {
                        count++;
                        notes.Add("Note "+count, e.Value);
                    }
                }
            }
            return notes;
        }
    }
}
