using System;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;
using Dblp.Domain.Interfaces;

namespace Dblp.WebUi.Models
{
    public class StatusViewModel

    {
        public StatusViewModel(LoadDetails loadDetails, List<StatusListElement> elements)
        {
            LoadDetails = loadDetails;
            Elements = elements;
        }
        public LoadDetails LoadDetails { get; set; }
        public List<StatusListElement> Elements { get; set; }

    }
}