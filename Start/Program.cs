using Dblp.ExtractBht;
using Dblp.ExtractBht.Saver;

namespace Start
{
    public class Program
    {
        public const string Startpath = @"D:\dblp\bht\db\conf\";
        static void Main(string[] args)
        {

            var extractor = new StructureExtractor();
            extractor.StartScan(Startpath);
            var saver = new EntityFrameWorkSaver();
            //var saver = new JsonSaver(@"D:\dblp\Structure.json");
            saver.SaveConference(extractor.Structures);
        }
    }
}
