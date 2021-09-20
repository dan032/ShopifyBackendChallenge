using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopifyBackendChallenge.Web.Data.Common;
using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Helpers;
using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Services.Common;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Services.Jwt
{
    public class JwtUserAuthentication : IUserAuthentication
    {
        private readonly IUserData _userData;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public JwtUserAuthentication(IUserData userData, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _userData = userData;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse> Authenticate(UserCreateDto model)
        {
            UserModel userModel = await _userData.GetUser(model.Password,_mapper.Map<UserModel>(model));
            UserReadDto user = _mapper.Map<UserReadDto>(userModel);

            if (user == null) return null;

            string token = GenerateJwtToken(user);
            return new AuthenticateResponse(token);
        }

        public UserModel GetById(int id)
        {
            return _userData.GetUserById(id);
        }

        private string GenerateJwtToken(UserReadDto user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim("sub", user.Id.ToString()),
                    new Claim("username", user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
