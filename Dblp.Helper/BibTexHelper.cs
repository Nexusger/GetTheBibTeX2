using System.Security;
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
            foreach (var node in x.Nodes())
            {
                result+= node.ToBibTeXAtribute();
            }
            result += "}";

            return result;
        }

        private static string ToBibTeXAtribute(this XNode xNode)
        {
            var attributeName = (xNode as XElement).Name;
            var attributeValue = (xNode as XElement).Value;
            var result = string.Format("  {0} = {{{1}}},\r\n", attributeName, attributeValue);
            return result;
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
