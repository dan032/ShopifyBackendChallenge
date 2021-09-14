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
        public Task<ImageModel> AddImageAsync(ImageModel image)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<int>> AddMultipleImagesMetadataAsync(IEnumerable<ImageModel> imageModels)
        {
            throw new NotImplementedException();
        }

        public Task<int> CommitAsync()
        {
            throw new NotImplementedException();
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
