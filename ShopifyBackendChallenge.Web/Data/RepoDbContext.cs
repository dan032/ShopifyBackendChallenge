using Microsoft.EntityFrameworkCore;
using ShopifyBackendChallenge.Web.Models;

namespace ShopifyBackendChallenge.Web.Data
{
    public class RepoDbContext : DbContext
    {
        public DbSet<MetadataModel> Images { get; set; }
        public DbSet<UserModel> Users{ get; set; }

        public RepoDbContext(DbContextOptions<RepoDbContext> options) : base(options)
        {

        }
    }
}
