using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Dblp.Helper;

namespace Dblp.Data.Extract
{
    public class XmlExtractor
    {
        public string InputFileName { get; set; }
        public string OutputFileName { get; set; }

        public Dictionary<string, string> SmallSearchResults = new Dictionary<string, string>();
        public XmlExtractor(string inputFileName)
        {
            InternRead(inputFileName);
        }
        public static IEnumerable<KeyValuePair<string, string>> Read(string inputFileName)
        {

            var setting = new XmlReaderSettings {DtdProcessing = DtdProcessing.Parse};
            using (XmlReader reader = XmlReader.Create(File.OpenText(inputFileName), setting))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name != "dblp")
                    {

                        foreach (var x in XmlHelper.GetChildContentElements(reader))
                        {
                            var key = x.Attribute("key").Value;
                            yield return new KeyValuePair<string, string>(key, x.ToString());
                        }
                    }

                }
            }
        }

        private void InternRead(string inputFileName)
        {
            var i = 0;
            foreach (var keyValuePair in Read(inputFileName))
            {
                SmallSearchResults.Add(keyValuePair.Key,keyValuePair.Value);
                i++;
                if (i % 10000 == 0)
                    Trace.TraceInformation("10-Tausend ({0})", i);
            }
        }

    }
}
