using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Dblp.Domain.Concrete;
using Dblp.Domain.Entities;
using Dblp.ExtractBht.Interface;

namespace Dblp.ExtractBht.Saver
{
    public class EntityFrameWorkSaver : IConferenceStructurSaver
    {

        private readonly long _batchSize;

        public EntityFrameWorkSaver(long batchSize)
        {
            _batchSize = batchSize;
        }
        public bool SaveConferences(IEnumerable<Conference> conferences)
        {
            var maxAmount = conferences.Count();
            var i = 0;
            var db = new EfDbContext();

            foreach (var conference in conferences)
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Conferences.Add(conference);
                Trace.TraceInformation("Prozent eingefügt: {0}", ((float)100 / (float)maxAmount) * (float)i);
                i++;
                if (i % _batchSize == 0)
                {
                    db.SaveChanges();
                    db.Dispose();
                    db = new EfDbContext();
                }
            }
            db.Dispose();
            return true;
        }
    }
}
