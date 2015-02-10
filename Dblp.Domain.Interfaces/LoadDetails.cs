using System;

namespace Dblp.Domain.Interfaces
{
    public class LoadDetails
    {
        public TimeSpan LoadTime { get; set; }
        public DateTime LastLoaded { get; set; }

        public long AmountBhtFilesParsed { get; set; }
        public long SizeOfXml { get; set; }
    }
}
