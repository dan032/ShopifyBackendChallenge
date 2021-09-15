using ShopifyBackendChallenge.Core.User;
using ShopifyBackendChallenge.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Services.common
{
    public interface IUserAuthentication
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        UserModel GetById(int id);
    }
}
