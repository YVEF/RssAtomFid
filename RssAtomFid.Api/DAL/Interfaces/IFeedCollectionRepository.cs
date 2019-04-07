
using RssAtomFid.Api.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.DAL.Interfaces
{
    public interface IFeedCollectionRepository
    {
        /// <summary>
        /// Show all feed short description for managing controller
        /// </summary>
        /// <returns></returns>
        IEnumerable<FeedSource> GetAllDiscoverFeed();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        Task<IEnumerable<FeedSource>> GetFeedSource(int tagId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        IEnumerable<DiscoverFeed> GetDiscoverFeedsByUserCollection(int userId, string collectionName);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<FeedsCollection> GetCollectionsByUser(int userId);
        /// <summary>
        /// Add new tag (method for admin)
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        Task AddNewTag(string tagName);

        IEnumerable<Tag> GetAllTags();


        Task AddFeedSource(FeedSource feed);
    }
}
