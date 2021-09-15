using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopifyBackendChallenge.Core.User;
using ShopifyBackendChallenge.Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopifyBackendChallenge.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            RepoDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RepoDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }


            HashSalt hashSalt = PasswordUtil.GenerateSaltedHash("test");
            UserModel user = new UserModel
            {
                Username = "test",
                Hash = hashSalt.Hash,
                Salt = hashSalt.Salt
            };

            if (!context.Users.Any())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
