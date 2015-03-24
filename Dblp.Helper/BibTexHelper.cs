using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Policy;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Dblp.Helper
{
    public static class BibTexHelper
    {
        public static string ToBibTeX(this string xElement)
        {
            var result = string.Empty;
            var x = XElement.Parse(xElement);
            var attributeName = x.Name;
            result = string.Format("@{0}{{{1},\r\n", attributeName, x.GetKey());
            var singleNodes = new List<XNode>();
            var nodeCounter = new Dictionary<string, int>();
            var multipleNodes = new List<XNode>();
            // Dblp url is internal link. External url attribute is called 'ee'
            foreach (var xNode in x.Nodes().Where(t=>(t as XElement).Name!="url"))
            {
                if (nodeCounter.ContainsKey((xNode as XElement).Name.ToString()))
                {
                    nodeCounter[(xNode as XElement).Name.ToString()]++;
                }
                else
                {
                    nodeCounter.Add((xNode as XElement).Name.ToString(),1);
                }
            }

            foreach (var kvp in nodeCounter.Where(t=>t.Value>1))
            {
                multipleNodes.AddRange(x.XPathSelectElements(kvp.Key));
            }

            var alreadyUsedMultipleNodeNames = new List<string>();
            foreach (var node in x.Nodes().Where(t => (t as XElement).Name != "url"))
            {
                if (nodeCounter[(node as XElement).Name.ToString()]==1)
                {
                    result += node.ToBibTeXAtribute();
                }
                else
                {
                    if (!alreadyUsedMultipleNodeNames.Contains((node as XElement).Name.ToString()))
                    {
                        result += MultiNodeAttributesToBibTex(multipleNodes, node);
                        alreadyUsedMultipleNodeNames.Add((node as XElement).Name.ToString());                        
                    }
                }
            }

            result = result.Remove(result.Length - 3) + "\r\n}";

            return result;
        }

        private static string MultiNodeAttributesToBibTex(List<XNode> multipleNodes, XNode node)
        {

            var attributeName = (node as XElement).Name.ToString();
            var result
            = attributeName+=" = {";
            result += (multipleNodes.FirstOrDefault() as XElement).Value;
            foreach (var multipleNode in multipleNodes.Where(t => (t as XElement).Name == (node as XElement).Name).Skip(1))
            {
                var attributeValue = (multipleNode as XElement).Value;
                result += " and " + attributeValue;
            }
            result += "},\r\n";



            return result;
        }

        private static string ToBibTeXAtribute(this XNode xNode)
        {
            var attributeName = ExtractAttributeName(xNode);
            var attributeValue = (xNode as XElement).Value;
            var result = string.Format("  {0} = {{{1}}},\r\n", attributeName, attributeValue);
            return result;
        }

        private static XName ExtractAttributeName(XNode xNode)
        {
            var attributeName = (xNode as XElement).Name;
            if (attributeName == "ee")
            {
                return "url";
            }
            return attributeName;
        }

        public static string GetKey(this XElement xElement)
        {
            var dblpKey = xElement.Attribute("key").Value;
            var splittedKey = dblpKey.Split('/');
            if
                (splittedKey.Length == 3)
            {
                return splittedKey[2];
            }
            return dblpKey;
        }
    }
}
