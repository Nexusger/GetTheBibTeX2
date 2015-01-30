using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Dblp.Helper
{
    public static class XmlHelper
    {
        public static string ExtractValue(this XElement xElement, string xpathSelector)
        {
            var element = xElement.XPathSelectElement(xpathSelector);
            if (element != null)
            {
                return element.Value;
            }
            return null;
        }

        public static string ExtractAttribute(this XElement xElement, string xpathSelector, string attibuteName)
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
        
        public static string ExtractTitleFromXElement(this XElement xelement)
        {
            //Step 1:
            var title = xelement.ExtractAttribute(".", "title");

            //Step 2:
            if (string.IsNullOrEmpty(title))
            {
                title = xelement.ExtractValue("h1");

            }

            return title;
        }
        public static string ExtractConferenceKey(this XElement xelement)
        {
            string conferenceKey;
            if (xelement.Attribute("key").Value.Contains(".bht"))
            {
                conferenceKey = xelement.Attribute("key").Value.Substring(0, xelement.Attribute("key").Value.Length - 4);
            }
            else
            {
                conferenceKey = xelement.Attribute("key").Value;
            }
            return conferenceKey;
        }
        public static string ExtractCiteKey(this XElement xelementConferences, string fileName)
        {
            var citeKey = xelementConferences.ExtractCiteKeyFromSubConferenceXElement() ??
                          fileName.ExtractCiteKeyFromFileName();
            return citeKey;
        }
        public static string ExtractCiteKeyFromSubConferenceXElement(this XElement xelement)
        {
            //Step 1:
            var citeKey = xelement.ExtractAttribute("cite", "key");

            //Step 2:
            if (string.IsNullOrEmpty(citeKey))
            {
                citeKey = xelement.ExtractAttribute("p/cite", "key");
            }

            //Step 3:
            if (string.IsNullOrEmpty(citeKey))
            {
                citeKey = xelement.ExtractAttribute(".", "key");
                //"db/conf/essen/essen91.bht"
                var splittedKey = citeKey.Split('/');
                var splittedKeyFile = splittedKey[splittedKey.Length - 1].Split('.');
                citeKey = splittedKey[splittedKey.Length - 3] + "/" + splittedKey[splittedKey.Length - 2] + "/" +
                          splittedKeyFile[0].ExtractYear();
            }

            return citeKey;
        }
        public static IEnumerable<XElement> GetChildContentElements(XmlReader reader)
        {
            // move to first child

            //reader.Read();

            while (true)
            {

                // skip whitespace between elements
                reader.MoveToContent();

                // break on end document section
                if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
                    yield break;
                var y = XElement.ReadFrom(reader);
                yield return (XElement)y;

            }
        }
    }

}
