using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dblp.WebUi.Models
{
    public class StatusListElement
    {
        public string DisplayText { get; set; }
        public string HoverText { get; set; }
        public bool SpecialMarker { get; set; }
        public long Value { get; set; }

    }
}