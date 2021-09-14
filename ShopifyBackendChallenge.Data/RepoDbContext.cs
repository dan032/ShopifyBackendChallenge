using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopifyBackendChallenge.Core.Image;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopifyBackendChallenge.Data
{
    public class RepoDbContext : IdentityDbContext
    {
        public DbSet<ImageModel> Images { get; set; }

        public RepoDbContext(DbContextOptions<RepoDbContext> options) : base(options)
        {

        }
    }
}
