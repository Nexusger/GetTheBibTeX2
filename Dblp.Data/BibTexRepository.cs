using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dblp.Data.Interfaces;
using Dblp.Helper;

namespace Dblp.Data
{
    public class BibTexRepository : IDblpBibTexRepository
    {
        private EfDbContext _context;

        public BibTexRepository(EfDbContext context)
        {
            _context = context;
        }

        public string GetBibTex(string dblpKey)
        {
            var bibTeXEntry = _context.BibTexEntries.FirstOrDefault(t => t.Key == dblpKey);
            if (bibTeXEntry != null && bibTeXEntry.Content!=null)
            {
                return bibTeXEntry.Content;
            }
            return "";
        }
    }
}
