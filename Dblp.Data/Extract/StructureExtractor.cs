﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Dblp.Data.Interfaces.Entities;
using Dblp.Helper;
using System.Collections.Concurrent;

namespace Dblp.Data.Extract
{
    public class StructureExtractor
    {
        public ConcurrentDictionary<Conference,byte> Structures { get; private set; }
    
        const string IndexBht = @"\index.bht";
        public StructureExtractor()
        {
            Structures = new ConcurrentDictionary<Conference,byte>();
        }

        public ConcurrentDictionary<Conference,byte> StartScan(string path)
        {
            Parallel.ForEach(Directory.GetDirectories(path), file =>
            {
                if (Directory.Exists(file))
                {
                    ProcessDirectory(file);
                }
            });
            return Structures;
        }

        private void ProcessDirectory(string path)
        {
            
            var indexFileName = path + IndexBht;
            if (!File.Exists(indexFileName))
            {
                Trace.TraceWarning("File {0} was not found. Ignoring",indexFileName);
                return;

            }

            var xelement = XElement.Load(indexFileName);
            
            var title = xelement.Attribute("title").Value;
            
            string conferenceKey = xelement.ExtractConferenceKey();

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(conferenceKey))
            {
                Trace.TraceWarning("Fehler: title ({0}) or conferenceKey ({1}) empty", title,conferenceKey);
                return;
            }

            var conference = new Conference(title, conferenceKey)
            {
                Abbr = conferenceKey.ExtractAbbreviationFromConferenceKey()
                //sometimes there is no special sub conference title. Take default
            };

            string subConferenceKey = title;
            //foreach (var xNode in xelement.Nodes())
            //{
            //    AddConferenceEvent(xNode, subConferenceKey, conference);
            //}
            conference.Events.AddRange(ExtracConferenceEvents(xelement, conference));
            Structures.GetOrAdd(conference,0);
            foreach (var fileName in FilesInPath(path))
            {
                try
                {
                    UpdateConferenceEvents(fileName, conference);
                }
                catch
                {
                    Trace.TraceWarning("Problems with updating Conference Events for Conference {0} from this file: {1}",conferenceKey,fileName);
                }
            }


        }
        private IEnumerable<string> FilesInPath(string path)
        {
            return Directory.GetFiles(path).Where(t => (!t.Contains(IndexBht) && !t.Contains("HTML") && t.EndsWith(".bht")));
        }

        private void AddConferenceEvent(XNode xNode, string subConferenceKey, Conference conference)
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

                    if (conference.Events.All(t => t.EventKey != subConferenceKey))
                    {
                        conference.Events.Add(
                            new ConferenceEvent
                            {
                                EventKey = elem.Attribute("key").Value,
                                //Setting dummy Value
                                Title = elem.Attribute("key").Value
                            });
                    }
                }
            }
        
        }

        private static List<ConferenceEvent> ExtracConferenceEvents(XElement xelement,Conference conference)
        {
            var conferenceEvents = new List<ConferenceEvent>();
            var conferenceEventKeys = xelement.XPathSelectElements("cite").Select(t=>t.Attribute("key").Value);
            foreach (var conferenceEventKey in conferenceEventKeys)
            {
                conferenceEvents.Add(new ConferenceEvent(){Confercence =  conference,EventKey = conferenceEventKey,Title = conferenceEventKey});
            }
            return conferenceEvents;
        }
        private static void UpdateConferenceEvents(string fileName, Conference conference)
        {
            XElement xelementConferences = XElement.Load(fileName);
            var citeKey = xelementConferences.ExtractCiteKey(fileName);
            var publicationElements =
                xelementConferences.XPathSelectElements("ul/li/cite")
                    .Select(t => new Publication { Key = t.Attribute("key").Value });
            var firstEvent = conference.Events.FirstOrDefault(t => t.EventKey == citeKey);
            if (firstEvent != null)
            {
                firstEvent.Publications =
                    publicationElements.ToList();
                firstEvent.Title = xelementConferences.ExtractTitleFromXElement();
            }
        }
    }
}
