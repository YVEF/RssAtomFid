using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RssAtomFid.Api.DAL.Entity;
using RssAtomFid.Api.DAL.Interfaces;
using RssAtomFid.Api.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.DAL.Repositories
{
    public class FeedCollectionRepository : IFeedCollectionRepository
    {

        private readonly ApplicationDbContext appContext;
        private readonly ILogger<FeedCollectionRepository> logger;

        public FeedCollectionRepository(ApplicationDbContext appContext, ILogger<FeedCollectionRepository> logger)
        {
            this.appContext = appContext;
            this.logger = logger;
        }

        public async Task AddTag(string tagName)
        {
            await appContext.Tags.AddAsync(new Tag() { Name = tagName });
            await appContext.SaveChangesAsync();
        }

        public IEnumerable<FeedSource> GetAllDiscoverFeed()
        {
            return appContext.FeedSources.AsNoTracking();
        }

        public async Task<IEnumerable<FeedsCollection>> GetCollectionsByUser(int userId)
        {
            return await appContext.FeedsCollections.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<FeedSource>> GetFeedSources(int tagId)
        {
            return await appContext.FeedSources.Where(x => x.TagId == tagId).ToListAsync();
        }

        public async Task<IEnumerable<FeedSource>> GetDiscoverFeedsByUserCollection(int userId, string collectionName)
        {
            var collection = await appContext.FeedsCollections.FirstOrDefaultAsync(x => x.Name == collectionName);
            return appContext.FeedSources.Where(x => x.FeedsCollectionId == collection.Id).AsNoTracking();
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return appContext.Tags.AsNoTracking();
        }


        public async Task AddFeedSource(FeedSource feed)
        {
            await appContext.FeedSources.AddAsync(feed);
            await appContext.SaveChangesAsync();
        }

        public async Task<FeedSource> GetFeedSource(int sourceId)
        {
            return await appContext.FeedSources.FirstOrDefaultAsync(x => x.Id == sourceId);
        }

        public async Task CreateCollection(FeedsCollection collection)
        {
            await appContext.FeedsCollections.AddAsync(collection);
            await appContext.SaveChangesAsync();
        }
    }
}
