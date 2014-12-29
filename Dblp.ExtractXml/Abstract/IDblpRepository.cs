using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temp.Entities;

namespace Temp.Abstract
{
    public interface IDblpRepository
    {
        IEnumerable<Person> Persons { get;  }
        void AddPerson(Person person);
        
        IEnumerable<Proceeding> Proceedings { get;  }
        void AddProceeding(Proceeding proceeding);
        IEnumerable<Inproceeding> Inproceedings { get;  }
        void AddInproceeding(Inproceeding inproceeding);
        IEnumerable<Book> Books { get; }
        void AddBook(Book book);
    }
}
