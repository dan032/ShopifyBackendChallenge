using ShopifyBackendChallenge.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(UserModel user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Token = token;
        }
    }
}
