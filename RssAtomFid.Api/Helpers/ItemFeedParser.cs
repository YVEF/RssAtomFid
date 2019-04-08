using RssAtomFid.Api.DAL.Entity;
using RssAtomFid.Api.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssAtomFid.Api.Helpers
{
    public class ItemFeedParser
    {
        public static Task<IEnumerable<FeedItem>> Parse(string url, FeedType feedType)
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

        private static IEnumerable<FeedItem> ParseAtom(string url)
        {
            try
            {
                XDocument document = XDocument.Load(url);
                if (document == null) throw new ArgumentNullException();
                return document.Root.Elements().Where(i => i.Name.LocalName == "entry").Select(item => new FeedItem
                {
                    Description = item.Elements().First(i => i.Name.LocalName == "content").Value,
                    Link = item.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value,
                    PubDate = SafeDateParse(item.Elements().First(i => i.Name.LocalName == "published").Value),
                    Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                });
            }
            catch
            {
                return null; ;
            }
        }

        private static IEnumerable<FeedItem> ParseRss(string url)
        {
            try
            {
                XDocument document = XDocument.Load(url);
                if (document == null) throw new ArgumentNullException();
                return document.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements()
                       .Where(i => i.Name.LocalName == "item").Select(item => new FeedItem
                       {
                           Description = item.Elements().First(i => i.Name.LocalName == "description").Value,
                           Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                           PubDate = SafeDateParse(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                           Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                       });
            }
            catch
            {
                return null;
            }
        }


        private static DateTime SafeDateParse(string date)
        {
            var success = DateTime.TryParse(date, out DateTime pubDate);
            if (success) return pubDate;

            return DateTime.Now;
        }
    }




}