using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.Helpers
{
    public static class SafeDateParse
    {
        public static DateTime Parse(string date)
        {
            var success = DateTime.TryParse(date, out DateTime pubDate);
            if (success) return pubDate;

            return DateTime.Now;
        }
    }
    
}
