using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopifyBackendChallenge.Core.Image;
using ShopifyBackendChallenge.Core.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopifyBackendChallenge.Data
{
    public class RepoDbContext : DbContext
    {
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<UserModel> Users{ get; set; }

        public RepoDbContext(DbContextOptions<RepoDbContext> options) : base(options)
        {

        }
    }
}
