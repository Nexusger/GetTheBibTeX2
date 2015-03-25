using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Dblp.Data;
using Dblp.Data.Extract;
using Dblp.Data.Interfaces.Entities;
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
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var loadDetails = new LoadDetails();

            Console.WriteLine("Starting to load files");
            //Save structure
            var repo = new InMemoryDataStore(PathToDblpXml, PathToBhtFolder);
            var saver = new EntityFrameWorkSaver(100);
            Console.WriteLine("Starting to save structure");
            //saver.SaveConferences(repo.Conferences);
            saver.SaveConferencesWithAddedAuthors(repo.Conferences, XmlExtractor.Read(PathToDblpXml).Where(k => (k.Value.StartsWith("<inproceeding")) || k.Value.StartsWith("<proceeding")));
            repo = null;
            saver = null;

            //Add Authors
            //Console.WriteLine("Updating datasets with authors");
            //saver = new EntityFrameWorkSaver(Settings.Default.BatchSize);
            //saver.UpdateStructureSetAuthor(XmlExtractor.Read(PathToDblpXml).Where(k => (k.Value.StartsWith("<inproceeding")) || k.Value.StartsWith("<proceeding")));

            //Add rawdata
            Console.WriteLine("Starting to load files");
            saver = new EntityFrameWorkSaver(Settings.Default.BatchSize);
            saver.SaveBibTexEntries(XmlExtractor.Read(PathToDblpXml).Where(k => (k.Value.StartsWith("<inproceeding"))||k.Value.StartsWith("<proceeding")).Select(t => new BibTexEntry()
            {
                Key = t.Key,
                Content = t.Value.ToBibTeX()
            }));
            stopwatch.Stop();

            loadDetails.LoadTime = stopwatch.Elapsed;
            loadDetails.SizeOfXml = new FileInfo(PathToDblpXml).Length;
            loadDetails.LastLoaded = DateTime.UtcNow;

            saver.SaveLoadDetails(loadDetails);
        }
    }
}
