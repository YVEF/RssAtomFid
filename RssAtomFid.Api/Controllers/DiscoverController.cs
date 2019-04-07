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
using RssAtomFid.Api.DAL.Interfaces;

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
            var feedSources = await feedsRepository.GetFeedSource(tag.Id);


            return Ok(feedSources);
        }

        [HttpGet("collections")]
        public async Task<IActionResult> GetCollections()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = feedsRepository.GetCollectionsByUser(currentUserId);
            return Ok();
        }

        [HttpGet("collections/{collectionName}")]
        public async Task<IActionResult> GetDiscovers([FromQuery] string collectionName)
        {
            var parseId = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId);
            if (parseId == false) return BadRequest();
            var result = feedsRepository.GetDiscoverFeedsByUserCollection(userId, collectionName);
            return Ok();
        }

        [HttpPost("{collectionName}")]
        public async Task<IActionResult> Post([FromBody] string collectionName)
        {
            return Ok();
        }


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
