using Microsoft.EntityFrameworkCore;
using ShopifyBackendChallenge.Web.Data;
using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Utils;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ShopifyBackendChallenge.Tests.Utils
{
    public class SharedDatabaseFixture : IDisposable
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;

        public SharedDatabaseFixture()
        {
            Connection = new SqlConnection(@"Server=db;Database=TestRepoDb;User=sa;Password=1Secure* Password1;");
            Seed();
            Connection.Open();
        }

        public DbConnection Connection { get; }

        public RepoDbContext CreateContext(DbTransaction transaction = null)
        {
            var context = new RepoDbContext(new DbContextOptionsBuilder<RepoDbContext>().UseSqlServer(Connection).Options);

            if  (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }
            return context;
        }

        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

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
                    _databaseInitialized = true;
                }
            }
        }

        public void Dispose() => Connection.Dispose();
    }
}
