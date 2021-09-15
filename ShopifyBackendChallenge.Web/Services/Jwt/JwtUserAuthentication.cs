using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopifyBackendChallenge.Core.User;
using ShopifyBackendChallenge.Data.Common;
using ShopifyBackendChallenge.Web.Helpers;
using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Services.common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Services.Jwt
{
    public class JwtUserAuthentication : IUserAuthentication
    {
        private readonly IUserData _userData;
        private readonly AppSettings _appSettings;

        public JwtUserAuthentication(IUserData userData, IOptions<AppSettings> appSettings)
        {
            _userData = userData;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            UserModel user = await _userData.GetUserByUsernameAndPasswordAsync(model.Username, model.Password);

            if (user == null) return null;

            string token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        public async Task<UserModel> GetById(int id)
        {
            return await _userData.GetUserById(id);
        }

        private string GenerateJwtToken(UserModel user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
