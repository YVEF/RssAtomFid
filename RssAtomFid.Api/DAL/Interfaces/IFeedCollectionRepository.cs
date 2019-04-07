
using RssAtomFid.Api.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.DAL.Interfaces
{
    public interface IFeedCollectionRepository
    {
        Task<IEnumerable<DiscoverFeed>> GetAllDiscoverFeed();
        IEnumerable<DiscoverFeed> GetDiscoverFeedByTag(string tagName);
        IEnumerable<DiscoverFeed> GetDiscoverFeedsByUserCollection(int userId, string collectionName);
        IEnumerable<string> GetCollectionsByUser();
    }
}
