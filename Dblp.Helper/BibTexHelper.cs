using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Policy;
using System.Xml.Linq;

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
            var multipleNodes = new List<XNode>();
            foreach (var xNode in x.Nodes())
            {
                if (!singleNodes.Any(t => (t as XElement).Name == (xNode as XElement).Name))
                {
                    singleNodes.Add(xNode);
                }
                else
                {
                    multipleNodes.Add(singleNodes.FirstOrDefault(t => (t as XElement).Name == (xNode as XElement).Name));
                    singleNodes.Remove(singleNodes.FirstOrDefault(t => (t as XElement).Name == (xNode as XElement).Name));
                    multipleNodes.Add(xNode);
                }
            }

            foreach (var node in x.Nodes())
            {
                if (singleNodes.Any(t => (t as XElement).Name == (node as XElement).Name))
                {
                    result += node.ToBibTeXAtribute();
                }
                else
                {
                    result += MultiNodeAttributesToBibTex(multipleNodes, node);
                }
            }



            result += "}";

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
            result += "}";



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
