using System.Linq;
using System.Xml.Linq;
using Temp.Abstract;
using Temp.Entities;

namespace Temp.Factories
{
    public class BookFactory
    {
        private readonly IDblpRepository _repository;

        public BookFactory(IDblpRepository repository)
        {
            _repository = repository;
        }

        public Book CreateBook(XElement element)
        {
            var book = new Book(element);
            var efBook = _repository.Books.FirstOrDefault(p => p.BookKey == book.BookKey);
            if (efBook != null)
                return efBook;

            //TODO Hier müssen noch die Autoren gefunden werden
            return book;
        }



    }
}
