using System.Configuration;
using System.Data.Entity.Infrastructure.Design;
using System.Net.Mime;
using Dblp.Data;
using Dblp.Data.Saver;

namespace Dblp.DataExtraction.Start
{
    public class Program
    {
        public const string PathToDblpXml = @"D:\dblp\dblp.xml";
        public const string PathToBhtFolder = @"D:\dblp\bht\db\conf\";
        static void Main(string[] args)
        {
            var repo = new InMemoryDataStore(PathToDblpXml, PathToBhtFolder);
            var saver = new EntityFrameWorkSaver(Settings.Default.BatchSize);
            saver.SaveConferences(repo.Conferences);
            
        }
    }
}
