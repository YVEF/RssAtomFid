using Microsoft.EntityFrameworkCore;
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

        public AuthRepository(ApplicationDbContext appContext) => this.appContext = appContext;

        public async Task<bool> Login(string username, string password)
        {
            var user = await appContext.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return false;

            return true;
        }

        

        public async Task<User> Register(User user, string password)
        {
            var tuplePasswordHashAndSalt = CalculatePasswordHash(password);
            user.PasswordHash = System.Text.Encoding.UTF8.GetString(tuplePasswordHashAndSalt.Item1);
            user.PasswordSalt = System.Text.Encoding.UTF8.GetString(tuplePasswordHashAndSalt.Item2);
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

        

        private bool VerifyPassword(string password, string passwordHash, string passwordSalt)
        {
            var passwordHashBytes = System.Text.Encoding.UTF8.GetBytes(passwordHash);
            var passwordSaltBytes = System.Text.Encoding.UTF8.GetBytes(passwordSalt);
            using (var hmac = new HMACSHA256(passwordSaltBytes))
            {
                var calculatedPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if (calculatedPasswordHash != passwordHashBytes) return false;
                //for (int i = 0; i < calculatedPasswordHash.Length; i++)
                //{

                //}
                return true;
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await appContext.Users.AnyAsync(x => x.UserName == username))
                return true;
            return false;
        }
    }
}
