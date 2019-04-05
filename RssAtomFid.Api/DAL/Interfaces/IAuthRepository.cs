using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RssAtomFid.Api.DAL.Entity.Account;

namespace RssAtomFid.Api.DAL.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<bool> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
