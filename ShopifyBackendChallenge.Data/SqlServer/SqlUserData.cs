using ShopifyBackendChallenge.Core.User;
using ShopifyBackendChallenge.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopifyBackendChallenge.Core.Utils;

namespace ShopifyBackendChallenge.Data.SqlServer
{
    public class SqlUserData : IUserData
    {
        private readonly RepoDbContext _dbContext;

        public SqlUserData(RepoDbContext context)
        {
            _dbContext = context;
        }

        public async Task<UserModel> AddUserAsync(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            return user;
        }

        public async Task<UserModel> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            try
            {
                IEnumerable<UserModel> users = await _dbContext.Users.ToListAsync();
                return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
