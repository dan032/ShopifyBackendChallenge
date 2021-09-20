using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ShopifyBackendChallenge.Web.Utils;
using ShopifyBackendChallenge.Web.Data;

namespace ShopifyBackendChallenge.Web.Data.SqlServer
{
    public class SqlUserData : IUserData
    {
        private readonly RepoDbContext _dbContext;

        public SqlUserData(RepoDbContext context)
        {
            _dbContext = context;
        }

        public async Task<UserModel> AddUserAsync(UserModel model)
        {
            UserModel existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            
            if (existingUser == null)
            {
                await _dbContext.Users.AddAsync(model);
                return model;
            }
            return null;
        }

        public async Task<UserModel> GetUser(string attemptedPassword, UserModel model)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (user != null && PasswordUtil.VerifyPassword(attemptedPassword, user.Hash, user.Salt))
            {
                return user;
            }
            return null ;
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public UserModel GetUserById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
