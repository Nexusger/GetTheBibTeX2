using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Temp.Abstract;
using Temp.Entities;
using Temp.Factories;

namespace Temp.Concrete
{
    public class SqlServerSaver:ISave
    {
        private IDblpRepository _repository;

        public SqlServerSaver(IDblpRepository repository)
        {
            _repository = repository;
        }


        private static List<Name> ElementListToNameList(IEnumerable<XElement> elements)
        {
            if (elements != null || !elements.Any())
                return null;

            return elements.Select(xElement => new Name(xElement.Value)).ToList();
        }

        private static List<Note> ElementListToNotesList(IEnumerable<XElement> elements)
        {
            if (elements != null || !elements.Any())
                return null;

            return elements.Select(xElement => new Note(xElement.Value)).ToList();
        }

        private static int i = 0;
        public void Save(XElement element)
        {
            {
                var key = element.Attribute("key").Value;
                var keySegements = key.Split('/');

                switch (element.Name.ToString())
                {
                    case "www":

                        if (keySegements[0] == "homepages")
                        {
                            var url = "";

                            if (element.Element("url") != null)
                                url = element.Element("url").Value;
                            //var t = new Person()
                            //{
                            //    Id = i,
                            //    //PersonKey = key,
                            //    KnownNames = ElementListToNameList(element.Elements("author")),
                            //    Notes = ElementListToNotesList(element.Elements("note")),
                            //    Url = url
                            //};

                            //_repository.AddPerson(t);

                            Console.WriteLine(i++);
                        }

                        break;
                    case "inproceedings":
                        break;
                        var inproceeding = new Inproceeding(element);
                        var g = _repository.Inproceedings.Where(d => d.InproceedingKey == inproceeding.InproceedingKey).FirstOrDefault();
                        if (g == null)
                        {
                            _repository.AddInproceeding(inproceeding);
                        }
                        else
                        {
                            Console.WriteLine("Achtung Doppelter Insert:" + inproceeding + " und " + g);
                        }

                        break;

                    case "proceedings":
                        break;
                        _repository.AddProceeding(new ProceedingFactory(_repository).CreateProceeding(element));
                        break;

                    case "incollection":
                        break;
                    case "book":
                        break;
                        _repository.AddBook(new BookFactory(_repository).CreateBook(element));
                        break;
                    case "article":
                        break;
                    case "phdthesis":
                        break;
                    case "mastersthesis":
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
