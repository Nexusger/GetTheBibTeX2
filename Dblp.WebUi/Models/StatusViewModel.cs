using System;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace Dblp.WebUi.Models
{
    public class StatusViewModel

    {
        public StatusViewModel(DateTime lastUpdated, List<StatusListElement> elements)
        {
            LastUpdated = lastUpdated;
            Elements = elements;
        }
        public DateTime LastUpdated { get; private set; }
        public List<StatusListElement> Elements { get; set; }

    }
}