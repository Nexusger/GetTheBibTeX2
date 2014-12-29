using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp.Entities
{
    public abstract class Publication
    {
        public string Key { get; set; }
        public DateTime MDate { get; set; }
        public PublicationTypeEnum PublType { get; set; }

    }
}
