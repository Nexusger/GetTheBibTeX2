using System.Linq;
using System.Threading.Tasks;
using Dblp.Data.Interfaces;
using Dblp.Data.Interfaces.Entities;
using KeyTitleLookUp = Dblp.Data.Extract.KeyTitleLookUp;
using StructureExtractor = Dblp.Data.Extract.StructureExtractor;

namespace Dblp.Data
{
    public class InMemoryDataStore :IDblpDataStore
    {
        private StructureExtractor _extractor;

        public InMemoryDataStore(string pathTodblpXml,string pathToBhtFolder )
        {
            _extractor = new StructureExtractor();
            _extractor.StartScan(pathToBhtFolder);

            var keyTitleLookUp = new KeyTitleLookUp(pathTodblpXml);

            Parallel.ForEach(_extractor.Structures, conference =>
            {
                foreach (var @event in conference.Events)
                {
                    if (@event.Publications != null)
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

        //public IEnumerable<SearchResult> SearchResults { get; private set; }

        public IQueryable<Conference> Conferences
        {
            get { return _extractor.Structures.AsQueryable(); }
        }

        public IQueryable<Publication> Publications { get; private set; }
        public IQueryable<Author> Authors { get; private set; }
        public IQueryable<ConferenceEvent> ConferenceEvents { get; private set; }
    }
}
