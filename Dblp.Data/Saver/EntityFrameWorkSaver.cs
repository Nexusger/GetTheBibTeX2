using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Dblp.Data.Interfaces;
using Dblp.Data.Interfaces.Entities;

namespace Dblp.Data.Saver
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
                Trace.TraceInformation("Prozent eingefügt: {0} ({1})", ((float)100 / (float)maxAmount) * (float)i, i);
                i++;
                if (i % _batchSize == 0)
                {
                    db.SaveChanges();
                    db.Dispose();
                    db = new EfDbContext();
                }
            }
            db.SaveChanges();
            db.Dispose();

            return true;
        }
    }
}
