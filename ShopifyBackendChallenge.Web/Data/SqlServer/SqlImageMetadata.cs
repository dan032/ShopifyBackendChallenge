using Microsoft.EntityFrameworkCore;
using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Data.SqlServer
{
    public class SqlImageMetadata : IImageMetadata
    {
        private readonly RepoDbContext _context;

        public SqlImageMetadata(RepoDbContext context)
        {
            _context = context;
        }

        public async Task<MetadataModel> AddImageMetadataAsync(MetadataModel image)
        {
            await _context.Images.AddAsync(image);
          
            return image;
        }

        public Task<IEnumerable<int>> AddMultipleImagesMetadataAsync(IEnumerable<MetadataModel> imageModels)
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

        public Task<IEnumerable<MetadataModel>> GetAllImageMetadataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MetadataModel>> GetImageMetadataByTagsAsync(string tag, int userId)
        {
            return await _context.Images.Where(i => i.Tags.Contains(tag) && (i.UserId == userId || i.Private == false)).ToListAsync();
        }

        public async Task<IEnumerable<MetadataModel>> GetImagesMetadataByUserIdAsync(int userId)
        {
            return await _context.Images.Where(i => i.UserId == userId).ToListAsync();
        }
    }
}
