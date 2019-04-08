using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RssAtomFid.Api.DAL.Entity;
using RssAtomFid.Api.DAL.Interfaces;
using RssAtomFid.Api.ModelsDto;

namespace RssAtomFid.Api.Controllers
{
    // Add admin role in the future

    /// <summary>
    /// Admins only
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagingSourceFeedController : ControllerBase
    {
        private readonly IFeedCollectionRepository feedsRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public ManagingSourceFeedController(IFeedCollectionRepository feedsRepository, IMapper mapper, 
            ILogger<ManagingSourceFeedController> logger, IConfiguration configuration)
        {
            this.feedsRepository = feedsRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpGet("tags")]
        public IActionResult GetTags()
        {
            logger.LogInformation("Start GetTags");
            var tags = feedsRepository.GetAllTags();
            logger.LogInformation("GetTags is successful complete");
            return Ok(tags);
        }

        // Add new Tag
        [HttpPost("tags")]
        public async Task<IActionResult> AddTag([FromBody] string tagName)
        {
            if (tagName == null) BadRequest();
            logger.LogInformation("AddTag  ==>" + tagName);
            await feedsRepository.AddTag(tagName);
            return StatusCode(201);
        }
        
        // Add new feed source by tag
        [HttpPost("{tagName}")]
        public async Task<IActionResult> AddFeedSource(string tagName, [FromBody] FeedSourceDto feedSourceDto)
        {
            var tag = feedsRepository.GetAllTags().FirstOrDefault(x => x.Name.ToLower() == tagName.ToLower());
            if (tag == null) BadRequest();

            var feedSource = mapper.Map<FeedSource>(feedSourceDto);
            logger.LogInformation(" feedSource.Type ==> " + feedSource.Type.ToString());
            feedSource.TagId = tag.Id;
            
            logger.LogInformation(" tag.Id ==> " + tag.Id);

            await feedsRepository.AddFeedSource(feedSource);
            return StatusCode(201);
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}