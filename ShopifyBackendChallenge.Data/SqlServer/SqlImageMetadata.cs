using ShopifyBackendChallenge.Core.Image;
using ShopifyBackendChallenge.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Data.SqlServer
{
    public class SqlImageMetadata : IImageMetadata
    {
        private readonly RepoDbContext _context;

        public SqlImageMetadata(RepoDbContext context)
        {
            _context = context;
        }

        public async Task<ImageModel> AddImageMetadataAsync(ImageModel image)
        {
            await _context.Images.AddAsync(image);
            return image;
        }

        public Task<IEnumerable<int>> AddMultipleImagesMetadataAsync(IEnumerable<ImageModel> imageModels)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<int>> DeleteImagesMetadataByUriAsync(IEnumerable<string> imageModels)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageModel>> GetAllImageMetadataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageModel>> GetImagesMetadataByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
