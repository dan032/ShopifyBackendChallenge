using AutoMapper;
using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Helpers;

namespace ShopifyBackendChallenge.Web.Profiles
{
    public class MetadataProfile : Profile
    {
        public MetadataProfile()
        {
            CreateMap<ImageCreateDto, MetadataModel>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom<TagsResolver>());
            CreateMap<MetadataModel, ImageReadDto>()
                .ForMember(x => x.ImageData, opt => opt.Ignore());
        }
    }
}
