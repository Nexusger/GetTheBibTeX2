using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Temp.Abstract;
using Temp.Entities;

namespace Temp.Concrete
{
    public class RedisRepository:IDblpRepository
    {
        private static ConnectionMultiplexer _redis;

        public RedisRepository()
        {
            _redis = ConnectionMultiplexer.Connect("192.168.172.129");
        }

        public IEnumerable<Person> Persons { get; private set; }
        public void AddPerson(Person person)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proceeding> Proceedings { get; private set; }
        public void AddProceeding(Proceeding proceeding)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Inproceeding> Inproceedings { get; private set; }
        public void AddInproceeding(Inproceeding inproceeding)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> Books { get; private set; }
        public void AddBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
