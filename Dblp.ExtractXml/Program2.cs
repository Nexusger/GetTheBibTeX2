using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Newtonsoft.Json;
using Temp.Abstract;
using Temp.Concrete;
using Temp.Entities;
using Temp.Factories;

namespace Temp
{

    internal class Program2
    {
        private static Dictionary<string, string> smallSearchResults = new Dictionary<string, string>();

        private static void Main(string[] args)
        {
            var i = 0;
            var setting = new XmlReaderSettings { DtdProcessing = DtdProcessing.Parse };
            using (XmlReader reader = XmlReader.Create(File.OpenText(@"D:\dblp\dblp.xml"), setting))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name != "dblp")
                    {

                        foreach (var x in GetChildContentElements(reader))
                        {
                            var key = x.Attribute("key").Value;
                            var title = x.XPathSelectElement("title");
                            if (title!=null)
                                smallSearchResults.Add(key, title.Value);
                            i++;
                            if (i % 10000 == 0)
                                Console.WriteLine("10-Tausend ({0})", i);
                        }
                    }

                }
            }
            //File.WriteAllText(@"D:\dblp\smallSearchResult.json", serializedObject);
            using (var stream = new FileStream(@"D:\dblp\smallSearchResult.json", FileMode.CreateNew))
            {

                StreamWriter writer = new StreamWriter(stream);
                JsonTextWriter jsonWriter = new JsonTextWriter(writer);

                JsonSerializer ser = new JsonSerializer();

                ser.Formatting = Newtonsoft.Json.Formatting.Indented;
                ser.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                ser.TypeNameHandling = TypeNameHandling.All;

                ser.Serialize(jsonWriter, smallSearchResults);

                jsonWriter.Flush();
            }
        }

        private static IEnumerable<XElement> GetChildContentElements(XmlReader reader)
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
