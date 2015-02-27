using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dblp.Domain.Interfaces
{
    public interface IDblpBibTexRepository
    {
        string GetBibTex(string dblpKey);
    }
}
