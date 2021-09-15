using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Data.Common
{
    public interface IImageRepo
    {
        Task<string> AddImageAsync(IFormFile image, string userId);
        Task<IEnumerable<byte[]>> GetImagesByUserIdAsync(string userId);
        Task<List<int>> RemoveAllUserImagesAsync(string userId);
        Task<int> CommitAsync();
    }
}
