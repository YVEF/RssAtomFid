using RssAtomFid.Api.DAL.Entity;
using RssAtomFid.Api.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssAtomFid.Api.Helpers
{
    public class DiscoverParser : ITypeParse<DiscoverFeed>
    {
        public Task<DiscoverFeed> Parse(string url, FeedType feedType)
        {
            return Task.Run(() =>
            {
                switch (feedType)
                {
                    case FeedType.RSS: return ParseRss(url);

                    case FeedType.Atom: return ParseAtom(url);

                    default: throw new NotSupportedException();
                }
            });
        }

        private DiscoverFeed ParseAtom(string url)
        {
            try
            {
                XDocument document = XDocument.Load(url);
                if (document == null) throw new ArgumentNullException();
                var dics = document.Root.Elements().FirstOrDefault(i => i.Name.LocalName == "feed");

                return new DiscoverFeed
                {
                    Description = dics.Element("title").Value,
                    Link = dics.Element("link").Attribute("href").Value,                    
                    Icon = dics.Element("icon").Value
                };
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        private DiscoverFeed ParseRss(string url)
        {
            try
            {

                XDocument document = XDocument.Load(url);
                if (document == null) throw new ArgumentNullException();

                var dics = document.Root.Elements().FirstOrDefault(i => i.Name.LocalName == "channel");
                var result = new DiscoverFeed
                {
                    Title = dics.Element("title").Value,
                    Description = dics.Element("description").Value,
                    Link = dics.Element("link").Value
                };

                return result;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
    }
}