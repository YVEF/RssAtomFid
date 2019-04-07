using Microsoft.EntityFrameworkCore;
using RssAtomFid.Api.DAL.Entity;
using RssAtomFid.Api.DAL.Entity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FeedsCollection> FeedsCollections { get; set; }
        public DbSet<FeedSource> FeedSources { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
