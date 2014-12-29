using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.Linq;
using Temp.Abstract;
using Temp.Concrete;
using Temp.Entities;
using Temp.Factories;

namespace Temp
{
    class Program1
    {
        static Dictionary<string, List<string>> tableFields = new Dictionary<string, List<string>>();
        static void Main1(string[] args)
        {
            var setting = new XmlReaderSettings { DtdProcessing = DtdProcessing.Parse };
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using (XmlReader reader = XmlReader.Create(File.OpenText(@"D:\dblp\dblp.xml"), setting))
            {
                //ISave saver = new RedisSaver();
                while (reader.Read())
                {
                    // move to body content
                    if (reader.NodeType == XmlNodeType.Element && reader.Name != "dblp")
                    {

                            foreach (var x in GetChildContentElements(reader))
                            {
                                var key = x.Name.ToString();
                                if (key == "www")
                                {
                                    if (x.Attribute("key").Value.StartsWith("homepages"))
                                    {
                                        key = "person";
                                    }
                                }
                                if (!tableFields.ContainsKey(key))
                                {
                                    tableFields.Add(key, new List<string>());
                                }
                                foreach (var xNode in x.Nodes())
                                {
                                    if(xNode is XElement)
                                        
                                    if (!tableFields[key].Contains((xNode as XElement).Name.ToString()))
                                    {
                                        tableFields[key].Add((xNode as XElement).Name.ToString());
                                    }
                                }
                            }
                        if (reader.LocalName != "sectPr")
                            Console.WriteLine("Ende");
                        //    throw new Exception("Final section properties element expected.");
                    }


                }

                using (var filewriter = new StreamWriter(@"d:\dblp\ddl.sql"))
                {
                    foreach (var f in tableFields)
                    {
                        filewriter.Write("create table "+f.Key+" ( ");
                        foreach (var l in f.Value)
                        {
                            filewriter.Write(l+" nvarchar(100)," );
                        }
                        filewriter.WriteLine("id integer);");
                    }
                }

                Console.WriteLine("It took {0} seconds",stopwatch.ElapsedMilliseconds/1000.0);
                
                
            }
            using (XmlReader reader = XmlReader.Create(File.OpenText(@"D:\dblp\dblp.xml"), setting))
            {
                using (var filewriter = new StreamWriter(@"d:\dblp\insert.sql"))
                {
                    //ISave saver = new RedisSaver();
                    while (reader.Read())
                    {
                        // move to body content
                        if (reader.NodeType == XmlNodeType.Element && reader.Name != "dblp")
                        {

                            foreach (var x in GetChildContentElements(reader))
                            {
                                var key = x.Name.ToString();
                                if (key == "www")
                                {
                                    if (x.Attribute("key").Value.StartsWith("homepages"))
                                    {
                                        key = "person";
                                    }
                                }
                                var localFields = new List<string>();
                                var localvalues = new List<string>();
                                foreach (var xNode in x.Nodes())
                                {
                                    if(xNode is XElement)
                                        
                                    if (!localFields.Contains((xNode as XElement).Name.ToString()))
                                    {
                                        localFields.Add((xNode as XElement).Name.ToString());
                                        localvalues.Add((xNode as XElement).Value.ToString());
                                    }
                                }
                                var fields = "";
                                var values = "";
                                foreach (var localField in localFields)
                                {
                                    fields += localField+", ";
                                }
                                foreach (var value in localvalues)
                                {
                                    values += "'"+value+"', ";
                                }
                                var insert = "insert into " + key + " (" + fields + " id) values ("+values+",0);";
                                filewriter.WriteLine(insert);
                            }
                        }
                    }
                }
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


        private static Dictionary<string, StreamWriter> files = new Dictionary<string, StreamWriter>();

        private static int i;
        private static void DblpEntryGenerator(XElement element)
        {
            
            if (element == null)
                throw new ArgumentNullException();
                    var key = element.Attribute("key").Value;
                var keySegements = key.Split('/');

            if (files.ContainsKey(element.Name.ToString()))
            {
                files[element.Name.ToString()].WriteLine(element);
            }
            else
            {
                files.Add(element.Name.ToString(),new StreamWriter(@"D:\dblp\" + element.Name+".xml",true));
                files[element.Name.ToString()].WriteLine("<"+element.Name.ToString()+">");
            }

          // saver.Save(element);
        }

    }
}
