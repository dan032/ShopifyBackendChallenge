using AutoMapper;
using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Models;
using System;

namespace ShopifyBackendChallenge.Web.Helpers
{
    public class TagsResolver : IValueResolver<ImageCreateDto, MetadataModel, string>
    {
        public string Resolve(ImageCreateDto source, MetadataModel destination, string destMember, ResolutionContext context)
        {
            return String.Join(",", source.Tags);
        }
    }
}