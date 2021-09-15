using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Data.Common
{
    public interface IImageRepo
    {
        Task<string> AddImageAsync(IFormFile image, int userId);
        Task<IEnumerable<byte[]>> GetImagesByUserIdAsync(int userId);
        Task<List<int>> RemoveAllUserImagesAsync(int userId);
        Task<int> CommitAsync();
    }
}
