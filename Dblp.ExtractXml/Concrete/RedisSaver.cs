using System;
using System.Xml.Linq;
using StackExchange.Redis;
using Temp.Abstract;

namespace Temp.Concrete
{
    public class RedisSaver :ISave
    {
        private static ConnectionMultiplexer _redis;

        public RedisSaver()
        {
            _redis = ConnectionMultiplexer.Connect("192.168.172.129");
            
        }
        public void Save(XElement element)
        {
            IDatabase db = _redis.GetDatabase();
            db.StringSetAsync(element.Attribute("key").Value, element.Value );
            
         //   Console.WriteLine(element.Attribute("key").Value);
        }
    }
}
