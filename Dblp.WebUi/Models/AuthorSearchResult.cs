using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dblp.Domain.Interfaces.Entities;

namespace Dblp.WebUi.Models
{
    public class AuthorSearchResult
    {
        public string AuthorName { get; set; }
        public List<SearchResult> Publications { get; set; }
    }
}
