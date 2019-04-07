using RssAtomFid.Api.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.ModelsDto.Account
{
    public class UserViewDto
    {
        //public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public ICollection<FeedsCollection> FeedCollections { get; set; }
    }
}
