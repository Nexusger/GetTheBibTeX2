using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Dblp.Domain.Concrete;
using Dblp.Domain.Entities;
using Newtonsoft.Json;

namespace ExtractBht
{
    class Program
    {
        private static int i = 0;
        static List<ConferenceStructure> structures = new List<ConferenceStructure>();
        static void Main(string[] args)
        {
            var context = new EfDbContext();
            const string rootFolder = @"D:\dblp\bht\db\";
            const string confFolder = rootFolder + @"conf\";
            foreach (var file in Directory.GetDirectories(confFolder))
            {
                if (Directory.Exists(file))
                {
                    ProcessDirectory(file);
                }
            }
            
            var serializedObject = JsonConvert.SerializeObject(structures);
            File.WriteAllText(@"D:\dblp\Structure.json", serializedObject);
        }

        private static void ProcessDirectory(string path)
        {
            var indexFileName = path + @"\index.bht";
            if (!File.Exists(indexFileName))
            {
                Console.WriteLine("Fehler: Kein Index File in Folder {0}",path);
                return;
            }

            #region indexLoading
            var xelement = XElement.Load(indexFileName);
            var title = xelement.Attribute("title").Value;
            string conferenceKey;
            if (xelement.Attribute("key").Value.Contains(".bht"))
            {
                conferenceKey = xelement.Attribute("key").Value.Substring(0, xelement.Attribute("key").Value.Length-4);
            }
            else
            {
            conferenceKey= xelement.Attribute("key").Value;
                
            }

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(conferenceKey))
            {
                Console.WriteLine("Fehler: title or conferenceKey empty");
                return;
            }

            var conference = new ConferenceStructure(title,conferenceKey);
            
            //sometimes there is no special sub conference title. Take default
                
            string subConferenceKey = title; 
            foreach (var xNode in xelement.Nodes())
            {
                if (xNode.NodeType == System.Xml.XmlNodeType.Element)
                {
                    var elem = (XElement)xNode;
            
                    if (elem.Name == "p")
                    {
                        elem = elem.FirstNode as XElement;
                    }
                    if (elem != null && elem.Name == "cite")
                    {
                        subConferenceKey = elem.Attribute("key").Value;

                        if (conference.SubConferences.All(t => t.Key != subConferenceKey))
                        {
                            conference.SubConferences.Add(
                                new SubConference()
                                {
                                    Key = elem.Attribute("key").Value
                                });
                        }
                    }
                }
            }
            structures.Add(conference);
            XElement xelementConferences;
            foreach (var fileName in Directory.GetFiles(path).Where(t=>(!t.Contains("index.bht")&&!t.Contains("HTML")&&t.EndsWith(".bht"))))
            {
                try
                {
                    xelementConferences = XElement.Load(fileName);
                    var citeKey = ExtractCiteKeyFromSubConferenceXElement(xelementConferences);
                    if (citeKey == null)
                    {
                        citeKey = ExtractCiteKeyFromFileName(fileName);
                    }
                    var publicationElements =
                        xelementConferences.XPathSelectElements("ul/li/cite").Select(t => new Publication(){Key=t.Attribute("key").Value});
                    var firstOrDefault = conference.SubConferences.FirstOrDefault(t => t.Key == citeKey);
                    if (firstOrDefault != null)
                    {
                        firstOrDefault.Publications =
                            publicationElements.ToList();
                        firstOrDefault.Title = ExtractTitleFromXElement(xelementConferences);
                    }
                }
                catch (Exception e)
                {

                    //V1: 1803 einträge ohne cite key

                    Console.WriteLine("Ein Fehler: kein Citekey vorhanden "+ ++i);
                }
            }

            #endregion

        }

        private static string ExtractCiteKeyFromFileName(string fileName)
        {
            var splittedFileName = fileName.Split('\\');
            var splittedFile = splittedFileName[splittedFileName.Length - 1].Split('.');
            return splittedFileName[splittedFileName.Length - 3] + "\\" + splittedFileName[splittedFileName.Length - 2] + "\\" + splittedFile[0];
        }

        private static string ExtractCiteKeyFromSubConferenceXElement(XElement xelement)
        {
            //Step 1:
            var citeKey = ExtractAttribute(xelement, "cite", "key");

            //Step 2:
            if (string.IsNullOrEmpty(citeKey))
            {
                citeKey = ExtractAttribute(xelement, "p/cite", "key");
            }

            //Step 3:
            if (string.IsNullOrEmpty(citeKey))
            {
                citeKey = ExtractAttribute(xelement, ".", "key");
                //"db/conf/essen/essen91.bht"
                var splittedKey= citeKey.Split('/');
                var splittedKeyFile = splittedKey[splittedKey.Length - 1].Split('.');
                citeKey = splittedKey[splittedKey.Length - 3] + "/" + splittedKey[splittedKey.Length - 2] + "/" +
                          ExtractYear(splittedKeyFile[0]);
            }

            return citeKey;
        }

        private static string ExtractTitleFromXElement(XElement xelement)
        {
            //Step 1:
            var title = ExtractAttribute(xelement, ".", "title");

            //Step 2:
            if (string.IsNullOrEmpty(title))
            {
                title = ExtractValue(xelement, "h1");
            }

            return title;
        }

        private static string ExtractYear(string nameAndYear)
        {
            var regex4 = new Regex(@"\d{4}");
            var year = regex4.Matches(nameAndYear);
            if (year.Count == 1)
            {
                return year[0].ToString();
            }
            var regex2 = new Regex(@"\d{2}");
            var year2 = regex2.Matches(nameAndYear);
            if (year2.Count == 1)
            {
                return "19"+year2[0].ToString();
            }
            return "";
        }

        private static string ExtractAttribute(XElement xElement,string xpathSelector,string attibuteName)
        {
            var element = xElement.XPathSelectElement(xpathSelector);
            if (element != null)
            {
                var attribute = element.Attribute(attibuteName);
                if (attribute != null && !string.IsNullOrEmpty(attribute.Value))
                {
                    return attribute.Value;
                }
            }
            return null;
        }
        private static string ExtractValue(XElement xElement, string xpathSelector)
        {
            var element = xElement.XPathSelectElement(xpathSelector);
            if (element != null)
            {
                    return element.Value;
            }
            return null;
        }
    }
}
