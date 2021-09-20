using AutoMapper;
using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Models;

namespace ShopifyBackendChallenge.Web.Profiles
{
    public class ImagesProfile : Profile
    {
        public ImagesProfile()
        {
            CreateMap<ImageCreateDto, ImageModel>();
            CreateMap<ImageModel, ImageReadDto>();              
        }
    }
}
