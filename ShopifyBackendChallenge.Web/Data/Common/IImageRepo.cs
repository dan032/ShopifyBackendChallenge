using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Data.Common
{
    public interface IImageRepo
    {
        Task<string> AddImageAsync(ImageModel model);
        Task<List<int>> RemoveAllUserImagesAsync(int userId);
        Task<int> CommitAsync();
        List<ImageReadDto> GetImagesFromMetadataAsync(int userId, IEnumerable<MetadataModel> metadata);
    }
}
