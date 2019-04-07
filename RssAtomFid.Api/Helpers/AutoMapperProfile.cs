using AutoMapper;
using RssAtomFid.Api.DAL.Entity;
using RssAtomFid.Api.DAL.Entity.Account;
using RssAtomFid.Api.ModelsDto;
using RssAtomFid.Api.ModelsDto.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserLoginDto, User>();
            CreateMap<User, UserViewDto>();
            CreateMap<DiscoverFeed, DiscoverFeedViewDto>();
            CreateMap<FeedsCollection, FeedsCollectionDto>();
            // CreateMap<FeedSourceDto, FeedSource>().ForMember(dest => dest.Type, option => option.MapFrom(src => src.Type));

            CreateMap<FeedSourceDto, FeedSource>();
        }
    }
}
