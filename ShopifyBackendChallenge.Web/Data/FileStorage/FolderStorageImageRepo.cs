using Microsoft.AspNetCore.Http;
using ShopifyBackendChallenge.Web.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.IO;
using ShopifyBackendChallenge.Web.Models;
using System.Linq;
using ShopifyBackendChallenge.Web.Dtos;

namespace ShopifyBackendChallenge.Web.Data.FileStorage
{
    public class FolderStorageImageRepo : IImageRepo
    {
        private readonly string rootFolder = @"/home/images";
        public async Task<string> AddImageAsync(ImageModel model)
        {

            string pathDirectory = Path.Combine(rootFolder, model.UserId.ToString());
            string filePrefix = Path.GetRandomFileName();
            string fileName = $"{filePrefix}.{Path.GetExtension(model.ImageData.FileName)}";

            Directory.CreateDirectory(pathDirectory);

            pathDirectory = Path.Combine(pathDirectory, fileName);

            if (!File.Exists(pathDirectory))
            {
                using (Stream fileStream = new FileStream(pathDirectory, FileMode.Create))
                {
                    await model.ImageData.CopyToAsync(fileStream);
                }
            }

            return pathDirectory;
        }

        public Task<int> CommitAsync()
        {
            return null;
        }

        public List<ImageReadDto> GetImagesFromMetadataAsync(int userId, IEnumerable<MetadataModel> data)
        {
            List<ImageReadDto> images = new List<ImageReadDto>();
            foreach(MetadataModel metadata in data)
            {
                var image = new ImageReadDto
                {
                    ImageData = File.ReadAllBytes(metadata.ImageUri),
                    UserId = userId,
                    Title = metadata.Title,
                    Description = metadata.Description,
                    Tags = metadata.Description,
                    Private = metadata.Private
                };
                images.Add(image);
                
            }

            return images;
        }

        public Task<List<int>> RemoveAllUserImagesAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
