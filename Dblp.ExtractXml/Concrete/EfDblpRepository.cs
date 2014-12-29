using System;
using System.Collections.Generic;
using Temp.Abstract;
using Temp.Entities;

namespace Temp.Concrete
{
    public class EfDblpRepository : IDblpRepository ,IDisposable
    {
        private readonly DblpContext _context = new DblpContext();
        public IEnumerable<Person> Persons {
            get { return _context.Persons; }
        }

        public void AddPerson(Person person)
        {
         //   Console.WriteLine("Person {0} hinzugefügt",person.PersonKey);
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public IEnumerable<Proceeding> Proceedings {
            get { return _context.Proceedings; }
        
        }

        public void AddProceeding(Proceeding proceeding)
        {
            Console.WriteLine("Proceeding {0} hinzugefügt", proceeding.ProceedingKey);
            _context.Proceedings.Add(proceeding);
            _context.SaveChanges();
        }

        public IEnumerable<Inproceeding> Inproceedings {
            get { return _context.Inproceedings; }
        }

        public void AddInproceeding(Inproceeding inproceeding)
        {
            Console.WriteLine("inproceeding {0} hinzugefügt", inproceeding.InproceedingKey);
            _context.Inproceedings.Add(inproceeding);
            _context.SaveChanges();
        }

        public IEnumerable<Book> Books
        {
            get { return _context.Books; }
        }

        public void AddBook(Book book)
        {
            Console.WriteLine("Buch {0} hinzugefügt", book.BookKey);
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            
            _context.Dispose();
        }
    }

}
