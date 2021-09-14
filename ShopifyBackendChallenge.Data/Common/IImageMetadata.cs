using ShopifyBackendChallenge.Core.Image;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Data.Common
{
    public interface IImageMetadata
    {
        Task<IEnumerable<ImageModel>> GetAllImageMetadataAsync();
        Task<IEnumerable<ImageModel>> GetImagesMetadataByUserIdAsync(string userId);
        Task<ImageModel> AddImageAsync(ImageModel image);
        Task<IEnumerable<int>> AddMultipleImagesMetadataAsync(IEnumerable<ImageModel> imageModels);
        Task<IEnumerable<int>> DeleteImagesMetadataByUriAsync(IEnumerable<string> imageModels);
        Task<int> CommitAsync();
    }
}
