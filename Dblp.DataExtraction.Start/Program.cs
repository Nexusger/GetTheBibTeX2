using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dblp.Data;
using Dblp.Data.Extract;
using Dblp.Data.Saver;
using Dblp.Helper;

namespace Dblp.DataExtraction.Start
{
    public class Program
    {
        public const string PathToDblpXml = @"D:\dblp\dblp.xml";
        public const string PathToBhtFolder = @"D:\dblp\bht\db\conf\";
        static void Main(string[] args)
        {
            //var repo = new InMemoryDataStore(PathToDblpXml, PathToBhtFolder);
            //var saver = new EntityFrameWorkSaver(Settings.Default.BatchSize);
            //saver.SaveConferences(repo.Conferences);
            //repo = null;
            //saver = null;

            var x = new XmlExtractor(@"D:\dblp\dblp-min.xml");
            var a = new List<string>();
            foreach (var y in x.SmallSearchResults.Where(t=>(!t.Key.Contains("persons")) && (!t.Key.Contains("www"))))
            {
                a.Add(y.Value.ToBibTeX());
            }
            File.WriteAllLines(@"c:\temp\bibtex2.bib",a);
        }
    }
}
