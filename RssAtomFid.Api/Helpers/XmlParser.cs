using RssAtomFid.Api.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.Helpers
{
    public class XmlParser<T>
    {
        private ITypeParse<T> parser;
        public XmlParser(ITypeParse<T> parser)
        {
            this.parser = parser;
        }
        public Task<T> Parse(string url, FeedType type)
        {
            return parser.Parse(url, type);
        }
    }
}
