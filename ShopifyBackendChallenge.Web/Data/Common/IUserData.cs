using ShopifyBackendChallenge.Web.Models;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Data.Common
{
    public interface IUserData
    {
        Task<UserModel> AddUserAsync(UserModel model);
        Task<UserModel> GetUser(string attemptedPassword, UserModel model);
        UserModel GetUserById(int id);
        Task<int> CommitAsync();
    }
}
