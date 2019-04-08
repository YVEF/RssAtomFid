using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.Helpers
{
    public static class DateTimeExtension
    {
        private static DateTime SafeDateParse(this DateTime time, string date)
        {
            var success = DateTime.TryParse(date, out DateTime pubDate);
            if (success) return pubDate;

            return DateTime.Now;
        }
    }
    
}
