using AutoMapper;
using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Helpers;

namespace ShopifyBackendChallenge.Web.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<UserCreateDto, UserModel>();
            CreateMap<UserModel, UserReadDto>();
        }
    }
}
