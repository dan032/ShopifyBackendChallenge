using Microsoft.AspNetCore.Http;
using ShopifyBackendChallenge.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

namespace ShopifyBackendChallenge.Data.FileStorage
{
    public class FolderStorageImageRepo : IImageRepo
    {
        private readonly string rootFolder = @"/home/images";
        public async Task<string> AddImageAsync(IFormFile image, int userId)
        {

            string pathDirectory = Path.Combine(rootFolder, userId.ToString());
            string filePrefix = Path.GetRandomFileName();
            string fileName = $"{filePrefix}.{Path.GetExtension(image.FileName)}";

            Directory.CreateDirectory(pathDirectory);

            pathDirectory = Path.Combine(pathDirectory, fileName);

            if (!File.Exists(pathDirectory))
            {
                using (Stream fileStream = new FileStream(pathDirectory, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }

            return pathDirectory;
        }

        public Task<int> CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<byte[]>> GetImagesByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<int>> RemoveAllUserImagesAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
