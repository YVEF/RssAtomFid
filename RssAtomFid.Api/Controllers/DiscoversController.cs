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
    public class DiscoversController : ControllerBase
    {
        private readonly IFeedCollectionRepository feedsRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private readonly IAuthRepository authRepository;

        public DiscoversController(IFeedCollectionRepository feedsRepository, IMapper mapper, ILogger<DiscoversController> logger,
            IAuthRepository authRepository, IConfiguration configuration)
        {
            this.feedsRepository = feedsRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.configuration = configuration;
            this.authRepository = authRepository;
        }

        // return tags list
        [HttpGet()]
        public IActionResult GetTags()
        {
            logger.LogInformation("Start GetTags");
            var tags = feedsRepository.GetAllTags();
            logger.LogInformation("GetTags is successful complete");
            return Ok(tags);
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
                var result = await DiscoverParser.Parse(item.Link, item.Type);
                if (result == null) continue;
                result.sourceId = item.Id;
                discovers.Add(result);
            }
            return Ok(discovers);
        }


        [HttpGet("{tagName}/{sourceId:int}")]
        public async Task<IActionResult> GetFeedItems([FromRoute] int sourceId)
        {
            var feedSource = await feedsRepository.GetFeedSource(sourceId);
            var result = await ItemFeedParser.Parse(feedSource.Link, feedSource.Type);
            
            return Ok(result);
        }


        [HttpGet("collections", Name = "GetCollections")]
        public async Task<IActionResult> GetCollections()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await feedsRepository.GetCollectionsByUser(currentUserId);
            return Ok(result);
        }

        [HttpGet("collections/{collectionName}")]
        public async Task<IActionResult> GetSources([FromRoute] string collectionName)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var feedSources = await feedsRepository.GetDiscoverFeedsByUserCollection(currentUserId, collectionName);

            var discovers = new List<DiscoverFeed>(feedSources.Count());
            foreach (var item in feedSources)
            {
                var result = await DiscoverParser.Parse(item.Link, item.Type);
                if (result == null) continue;
                result.sourceId = item.Id;
                discovers.Add(result);
            }
            return Ok(discovers);
        }

        [HttpPost("collections")]
        public async Task<IActionResult> Post([FromBody] CollectionCreate newCollection)
        {
            try
            {
                if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
                var feedsCollection = mapper.Map<FeedsCollection>(newCollection);
                feedsCollection.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                await feedsRepository.CreateCollection(feedsCollection);
                return StatusCode(201);
            }
            catch
            {
                return StatusCode(400, "Incorrect request");
            }
            
        }

        // Add feed source to collection
        [HttpPut("collections/{collectionName}")]
        public async Task<IActionResult> Put([FromRoute] string collectionName, [FromBody] int sourceId)
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                logger.LogInformation("current user    |==>" + currentUserId);
                await feedsRepository.AddFeedSourceToCollection(currentUserId, collectionName, sourceId);
                return StatusCode(201);
            }
            catch
            {
                return StatusCode(400, "Incorrect request");
            }

        }

        [HttpDelete("collections")]
        public IActionResult Delete([FromBody] string collectionName)
        {
            var result = feedsRepository.DeleteCollection(collectionName);
            if (result == false) return BadRequest("nonexistent collection");
            return CreatedAtAction("GetCollections", StatusCode(200));
        }
    }
}
