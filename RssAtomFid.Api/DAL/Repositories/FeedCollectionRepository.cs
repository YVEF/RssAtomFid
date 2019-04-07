﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RssAtomFid.Api.DAL.Entity;
using RssAtomFid.Api.DAL.Interfaces;
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

        public async Task AddNewTag(string tagName)
        {
            await appContext.Tags.AddAsync(new Tag() { Name = tagName });
            await appContext.SaveChangesAsync();
        }

        public IEnumerable<FeedSource> GetAllDiscoverFeed()
        {
            return appContext.FeedSources.AsNoTracking();
        }

        public IEnumerable<FeedsCollection> GetCollectionsByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FeedSource>> GetFeedSource(int tagId)
        {
            return await appContext.FeedSources.Where(x => x.TagId == tagId).ToListAsync();
        }

        public IEnumerable<DiscoverFeed> GetDiscoverFeedsByUserCollection(int userId, string collectionName)
        {
            throw new NotImplementedException();
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
    }
}
