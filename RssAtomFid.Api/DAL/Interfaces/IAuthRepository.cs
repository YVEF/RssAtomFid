using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RssAtomFid.Api.DAL.Entity.Account;

namespace RssAtomFid.Api.DAL.Interfaces
{
    public interface IAuthRepository
    {
        /// <summary>
        /// Register method without cheking of user exists
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> Register(User user, string password);
        /// <summary>
        /// Just login method
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> Login(string username, string password);
        /// <summary>
        /// Check user exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> UserExists(string email);

        Task<User> GetUser(string email);

        Task<User> GetUser(int id);
    }
}
