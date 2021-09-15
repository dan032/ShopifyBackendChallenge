using ShopifyBackendChallenge.Core.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Data.Common
{
    public interface IUserData
    {
        Task<UserModel> AddUserAsync(UserModel user);
        Task<UserModel> GetUserByUsernameAndPasswordAsync(string username, string password);
        Task<UserModel> GetUserById(int id);
        Task<int> CommitAsync();
    }
}
