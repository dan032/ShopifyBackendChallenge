using Microsoft.AspNetCore.Http;
using ShopifyBackendChallenge.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Data.FileStorage
{
    public class FolderStorageImageRepo : IImageRepo
    {
        public Task<string> AddImageAsync(IFormFile image, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<byte[]>> GetImagesByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<int>> RemoveAllUserImagesAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
