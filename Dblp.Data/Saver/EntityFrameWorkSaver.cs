using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dblp.Data.Interfaces;
using Dblp.Data.Interfaces.Entities;
using Dblp.Helper;

namespace Dblp.Data.Saver
{
    public class EntityFrameWorkSaver : IConferenceStructurSaver, IBibTeXSaver, IDetailSaver
    {

        private readonly long _batchSize;

        public EntityFrameWorkSaver(long batchSize)
        {
            _batchSize = batchSize;
        }

        public bool SaveConferences(IEnumerable<Conference> conferences)
        {
            var i = 0;
            var db = new EfDbContext();
            var sw = new Stopwatch();
            sw.Start();
            foreach (var conference in conferences)
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Conferences.Add(conference);
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
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds / 1000);
            return true;

        }

        public bool SaveConferencesWithAddedAuthors(IEnumerable<Conference> conferences, IEnumerable<KeyValuePair<string, string>> entries)
        {
            //Publicatione projezieren
            var sw1 = new Stopwatch();
            sw1.Start();
            var publicationsWithoutAuthors = new Dictionary<string, Publication>();
            foreach (var conference in conferences)
            {
                foreach (var @event in conference.Events)
                {
                    if (@event.Publications != null)
                    {

                        foreach (var publication in @event.Publications)
                        {
                            if (!publicationsWithoutAuthors.ContainsKey(publication.Key))
                                publicationsWithoutAuthors.Add(publication.Key, publication);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Keine Publicationen: " + @event.EventKey);
                    }
                }
            }
            sw1.Stop();
            Console.WriteLine("sw1 = " + (int)sw1.ElapsedMilliseconds / 1000);

            //Authors Updaten
            var sw2 = new Stopwatch();
            sw2.Start();
            Parallel.ForEach(entries, entry =>
            {

                Publication publication = new Publication();
                if (publicationsWithoutAuthors.ContainsKey(entry.Key))
                {
                    publication =
                        publicationsWithoutAuthors[entry.Key];
                    if (publication == null) return;

                }

                var xelement = XElement.Parse(entry.Value);
                var authors = xelement.ExtractAuthors();
                if (authors.Any())
                {
                    publication.Authors.AddRange(
                        authors.Select(k => new Author() { Name = k, Publication = publication }));
                }
            });
            sw2.Stop();
            Console.WriteLine("sw2 = " + (int)sw2.ElapsedMilliseconds / 1000);

            //Context speichern

            var i = 0;
            var db = new EfDbContext();
            var sw3 = new Stopwatch();
            sw3.Start();
            foreach (var conference in conferences)
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Conferences.Add(conference);
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
            sw3.Stop();
            Console.WriteLine("sw3 = " + sw3.ElapsedMilliseconds / 1000);
            return true;
        }


        public bool SaveBibTexEntries(IEnumerable<BibTexEntry> entries)
        {
            var i = 0;
            var db = new EfDbContext();

            foreach (var entry in entries)
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.BibTexEntries.Add(entry);
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

        public bool UpdateStructureSetAuthor(IEnumerable<KeyValuePair<string, string>> entries)
        {
            var i = 0;
            var db = new EfDbContext();

            foreach (var entry in entries)
            {
                db.Configuration.AutoDetectChangesEnabled = true;
                db.Configuration.ValidateOnSaveEnabled = true;

                var xelement = XElement.Parse(entry.Value);
                var authors = xelement.ExtractAuthors();
                if (authors.Any())
                {

                    var publication = db.Publications.FirstOrDefault(t => t.Key == entry.Key);
                    if (publication != null)
                    {
                        publication.Authors.AddRange(authors.Select(k => new Author() { Name = k, Publication = publication }));
                        i++;
                        if (i % _batchSize == 0)
                        {
                            db.SaveChanges();
                            db.Dispose();
                            db = new EfDbContext();
                        }
                        if (i % 100000 == 0)
                        {
                            Console.WriteLine("100K Done");
                        }

                    }


                }


            }


            db.SaveChanges();
            db.Dispose();
            return true;
        }

        public bool SaveLoadDetails(LoadDetails loadDetails)
        {
            var db = new EfDbContext();
            db.LoadDetails.Add(loadDetails);
            db.SaveChanges();
            return true;
        }
    }
}
