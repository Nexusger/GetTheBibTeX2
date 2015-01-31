using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Dblp.Helper;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Dblp.ExtractXml
{
    public class KeyTitleLookUp
    {
        public string InputFileName { get; set; }
        public string OutputFileName { get; set; }

        public Dictionary<string,string> SmallSearchResults = new Dictionary<string, string>();

        public KeyTitleLookUp(string inputFileName)
        {
            InternRead(inputFileName);
        }

        public static IEnumerable<KeyValuePair<string, string>> Read(string inputFileName)
        {

            var setting = new XmlReaderSettings { DtdProcessing = DtdProcessing.Parse };
            using (XmlReader reader = XmlReader.Create(File.OpenText(inputFileName), setting))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name != "dblp")
                    {

                        foreach (var x in XmlHelper.GetChildContentElements(reader))
                        {
                            var key = x.Attribute("key").Value;
                            var title = x.XPathSelectElement("title");
                            if (title != null)
                                yield return new KeyValuePair<string, string>(key, title.Value);
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

        public void Save(string outputFileName)
        {
            if (outputFileName == null)
                throw new ArgumentNullException("outputFileName");
            using (var stream = new FileStream(OutputFileName, FileMode.CreateNew))
            {

                StreamWriter writer = new StreamWriter(stream);
                JsonTextWriter jsonWriter = new JsonTextWriter(writer);

                JsonSerializer ser = new JsonSerializer();

                ser.Formatting = Formatting.Indented;
                ser.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                ser.TypeNameHandling = TypeNameHandling.All;

                ser.Serialize(jsonWriter, SmallSearchResults);

                jsonWriter.Flush();
            }
        }
    }
}
