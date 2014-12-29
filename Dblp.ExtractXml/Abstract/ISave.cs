using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Temp.Abstract
{
    public interface ISave
    {
        void Save(XElement element);
    }
}
