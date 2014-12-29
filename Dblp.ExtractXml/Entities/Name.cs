using Temp.Entities;

namespace Temp
{
    public class Name
    {
        public long Id { get; set; }
        public Name(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
        public virtual Person Person { get; set; }
    }
}