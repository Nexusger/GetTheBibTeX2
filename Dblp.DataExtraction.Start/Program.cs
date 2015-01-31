using System.Threading.Tasks;
using Dblp.ExtractBht;
using Dblp.ExtractXml;

namespace Start
{
    public class Program
    {
        public const string Startpath = @"D:\dblp\bht\db\conf\";
        static void Main(string[] args)
        {
            var extractor = new StructureExtractor();
            extractor.StartScan(Startpath);

            var keyTitleLookUp = new KeyTitleLookUp(@"D:\dblp\dblp.xml");

            Parallel.ForEach(extractor.Structures, conference =>
            {
                foreach (var @event in conference.Events)
                {
                    if(@event.Publications!=null)
                    foreach (var publication in @event.Publications)
                    {
                        if (publication != null)
                        {
                            if (keyTitleLookUp.SmallSearchResults.ContainsKey(publication.Key))
                            {
                                publication.Title = keyTitleLookUp.SmallSearchResults[publication.Key];
                            }
                        }
                    }
                }
            });
        }
    }
}
