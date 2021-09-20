using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Services.Common;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task Invoke(HttpContext context, IUserAuthentication userService)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                attachUserToContext(context, userService, token);
            }

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IUserAuthentication userService, string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)validatedToken;
                int userId = int.Parse(jwtSecurityToken.Claims.First(x => x.Type == "sub").Value);
                context.Items["User"] = _mapper.Map<UserReadDto>(userService.GetById(userId));
            }
            catch
            {

            }
        }
    }
}
