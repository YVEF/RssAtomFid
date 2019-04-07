using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RssAtomFid.Api.DAL.Interfaces;

namespace RssAtomFid.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscoverController : ControllerBase
    {
        private readonly IFeedCollectionRepository feedsRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public DiscoverController(IFeedCollectionRepository feedsRepository, IMapper mapper, ILogger<DiscoverController> logger,
            IConfiguration configuration)
        {
            this.feedsRepository = feedsRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.configuration = configuration;
        }


        [HttpGet("tagName")]
        public ActionResult<string> Get([FromQuery] string tagName)
        {
            var result = feedsRepository.GetDiscoverFeedByTag(tagName);
            return Ok();
        }

        [HttpGet("collections")]
        public ActionResult<string> GetCollections()
        {
            var result = feedsRepository.GetCollectionsByUser();
            return Ok();
        }

        [HttpGet("tagName")]
        public ActionResult<string> Get([FromQuery] string collectionName)
        {
            var result = feedsRepository.GetDiscoverFeedByTag(tagName);
            return Ok();
        }

        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
