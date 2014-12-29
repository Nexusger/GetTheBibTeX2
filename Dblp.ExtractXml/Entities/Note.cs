using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temp.Entities;

namespace Temp
{
    public class Note
    {
        public long Id { get; set; }
        public Note(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
        public virtual Person Person { get; set; }
    }
}
