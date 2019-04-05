using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RssAtomFid.Api.DAL.Entity.Account;
using RssAtomFid.Api.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RssAtomFid.Api.DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext appContext;
        private readonly ILogger<AuthRepository> logger;

        public AuthRepository(ApplicationDbContext appContext, ILogger<AuthRepository> logger)
        {
            this.appContext = appContext;
            this.logger = logger;
        }



        public async Task<User> Login(string email, string password)
        {
            logger.LogInformation("Start Login method in AuthRepository ||||||||||");

            var user = await appContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                logger.LogError("User was null ||||");
                return null;
            }

            logger.LogInformation("Login method was successful");
            return user;
        }
        

        public async Task<User> Register(User user, string password)
        {
            var tuplePasswordHashAndSalt = CalculatePasswordHash(password);
            user.PasswordHash = tuplePasswordHashAndSalt.Item1;
            user.PasswordSalt = tuplePasswordHashAndSalt.Item2;
            await appContext.Users.AddAsync(user);
            await appContext.SaveChangesAsync();
            return user;
        }

        
        private ValueTuple<byte[], byte[]> CalculatePasswordHash(string password)
        {
            using (var hmacsha = new HMACSHA256())
            {
                var passwordSalt = hmacsha.Key;
                var passwordHash = hmacsha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (passwordHash, passwordSalt);
            }
        }

        

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            logger.LogInformation("VerifyPassword method run |==>");

            using (var hmac = new HMACSHA256(passwordSalt))
            {
                var calculatedPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                
                for (int i = 0; i < calculatedPasswordHash.Length; i++)
                {
                    logger.LogInformation("VerifyPassword is faled  |==>");
                    if (calculatedPasswordHash[i] != passwordHash[i]) return false;
                }
                logger.LogInformation("VerifyPassword is successful  |==>");
                return true;
            }
        }

        public async Task<bool> UserExists(string email)
        {
            if (await appContext.Users.AnyAsync(x => x.Email == email))
                return true;
            return false;
        }
    }
}
