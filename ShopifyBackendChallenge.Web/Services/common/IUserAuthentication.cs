using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Models;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Services.Common
{
    public interface IUserAuthentication
    {
        Task<AuthenticateResponse> Authenticate(UserCreateDto model);
        UserModel GetById(int id);
    }
}
