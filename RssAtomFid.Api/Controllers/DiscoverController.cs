using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RssAtomFid.Api.DAL.Entity;
using RssAtomFid.Api.DAL.Interfaces;
using RssAtomFid.Api.Helpers;
using RssAtomFid.Api.ModelsDto;

namespace RssAtomFid.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]/")]
    [ApiController]
    public class DiscoverController : ControllerBase
    {
        private readonly IFeedCollectionRepository feedsRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private readonly IAuthRepository authRepository;

        public DiscoverController(IFeedCollectionRepository feedsRepository, IMapper mapper, ILogger<DiscoverController> logger,
            IAuthRepository authRepository, IConfiguration configuration)
        {
            this.feedsRepository = feedsRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.configuration = configuration;
            this.authRepository = authRepository;
        }


        [HttpGet("{tagName}")]
        public async Task<IActionResult> Get([FromRoute] string tagName)
        {
            logger.LogInformation(tagName);
            var tag = feedsRepository.GetAllTags().First(x => x.Name.ToLower() == tagName.ToLower());
            var feedSources = await feedsRepository.GetFeedSources(tag.Id);

            var discovers = new List<DiscoverFeed>(feedSources.Count());
            foreach(var item in feedSources)
            {
                var result = await DiscoverParser.Parse(item.Link, (Helpers.FeedType)item.Type);
                result.SourceId = feedSources.First().Id;
                discovers.Add(result);
            }
            return Ok(discovers);
        }

        [HttpGet("{tagName}/{sourceId:int}")]
        public async Task<IActionResult> GetFeedItems([FromRoute] int sourceId)
        {
            var feedSource = await feedsRepository.GetFeedSource(sourceId);
            var result = await ItemParser.Parse(feedSource.Link, (Helpers.FeedType)feedSource.Type);
            return Ok(result);
        }


        [HttpGet("collections")]
        public async Task<IActionResult> GetCollections()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await feedsRepository.GetCollectionsByUser(currentUserId);
            return Ok(result);
        }

        [HttpGet("collections/{collectionName}")]
        public async Task<IActionResult> GetDiscovers([FromRoute] string collectionName)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = feedsRepository.GetDiscoverFeedsByUserCollection(currentUserId, collectionName);
            return Ok();
        }

        [HttpPost("collections")]
        public async Task<IActionResult> Post([FromBody] CollectionCreate newCollection)
        {
            var feedsCollection = mapper.Map<FeedsCollection>(newCollection);
            feedsCollection.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            feedsCollection.TagId = feedsRepository.GetAllTags().First(x => x.Name == newCollection.TagName).Id;

            await feedsRepository.CreateCollection(feedsCollection);
            return StatusCode(201);
        }

        // Add feed source to collection
        [HttpPut("collections/{collectionName}")]
        public async Task<IActionResult> Put([FromQuery] string collectionName)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            logger.LogInformation("current user    |==>" + currentUserId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
