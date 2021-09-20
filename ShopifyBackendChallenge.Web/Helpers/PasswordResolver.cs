using AutoMapper;
using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Utils;

namespace ShopifyBackendChallenge.Web.Helpers
{
    public class PasswordHashResolver : IValueResolver<UserCreateDto, UserModel, HashSalt>
    {
        public HashSalt Resolve(UserCreateDto source, UserModel destination, HashSalt destMember, ResolutionContext context)
        {
            return PasswordUtil.GenerateSaltedHash(source.Password);
        }
    }

    public class PasswordSaltResolver : IValueResolver<UserCreateDto, UserModel, string>
    {
        public string Resolve(UserCreateDto source, UserModel destination, string destMember, ResolutionContext context)
        {
            return PasswordUtil.GenerateSaltedHash(source.Password).Salt;
        }
    }
}
