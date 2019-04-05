using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RssAtomFid.Api.DAL.Entity.Account;
using RssAtomFid.Api.DAL.Interfaces;
using RssAtomFid.Api.ModelsDto;
using RssAtomFid.Api.ModelsDto.Account;

namespace RssAtomFid.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public AccountController(IAuthRepository authRepository, IMapper mapper, ILogger<AccountController> logger, 
            IConfiguration configuration)
        {
            this.authRepository = authRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegDto)
        {
            logger.LogInformation(nameof(userRegDto), "start Register");
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var userReg = mapper.Map<User>(userRegDto);
            if (await authRepository.UserExists(userReg.Email))
            {
                logger.LogError(nameof(userRegDto), "Email or Password already exists");
                return BadRequest(new { Summury = "Email or Password already exists" });
            }
            var userResult = await authRepository.Register(userReg, userRegDto.Password);

            if (userResult == null) return BadRequest("Some error with register");

            logger.LogInformation("User Register was successful");
            return Ok(userResult);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var userLogin = await authRepository.Login(userLoginDto.Email, userLoginDto.Password);

            if(userLogin == null)
            {
                logger.LogError("User is null");
                ModelState.AddModelError(nameof(userLogin), "Email or Password was wrong");
                return new UnprocessableEntityObjectResult(ModelState);
            }
            logger.LogInformation("Login process was successful");
            return Ok(new
            {
                token = GenerateJwtToken(userLogin),
                user = userLogin
            });
        }


        private string GenerateJwtToken(User user)
        {
            logger.LogInformation("Add new Claim");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            //var roles = await _userManager.GetRolesAsync(user);

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(configuration.GetSection("SecuritySettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(5),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}