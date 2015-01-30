using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Dblp.Domain.Concrete;
using Dblp.Domain.Entities;
using Dblp.ExtractBht.Interface;

namespace Dblp.ExtractBht.Saver
{
    public class EntityFrameWorkSaver:IConferenceStructurSaver
    {

        private readonly long _batchSize;

        public EntityFrameWorkSaver(long batchSize)
        {
            _batchSize = batchSize;
        }
        public bool SaveConference(IEnumerable<Conference> conferences)
        {
            var maxAmount = conferences.Count();
            var i = 0;
            using (var db = new EfDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                foreach (var conference in conferences)
                {
                    db.Conferences.Add(conference);
                    Trace.TraceInformation("Prozent eingefügt: {0}", ((float) 100/(float) maxAmount)*(float) i);
                    i++;
                    if (i % _batchSize == 0)
                        db.SaveChanges();
                }
            }
            return true;
        }
    }
}
