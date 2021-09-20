using ShopifyBackendChallenge.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Data.Common
{
    public interface IImageMetadata
    {
        Task<IEnumerable<MetadataModel>> GetAllImageMetadataAsync();
        Task<IEnumerable<MetadataModel>> GetImagesMetadataByUserIdAsync(int userId);
        Task<MetadataModel> AddImageMetadataAsync(MetadataModel image);
        Task<IEnumerable<int>> AddMultipleImagesMetadataAsync(IEnumerable<MetadataModel> imageModels);
        Task<IEnumerable<int>> DeleteImagesMetadataByUriAsync(IEnumerable<string> imageModels);
        Task<IEnumerable<MetadataModel>> GetImageMetadataByTagsAsync(string tag, int userId);
        Task<int> CommitAsync();
    }
}
