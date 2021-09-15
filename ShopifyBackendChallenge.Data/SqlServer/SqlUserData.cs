﻿using ShopifyBackendChallenge.Core.User;
using ShopifyBackendChallenge.Data.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopifyBackendChallenge.Data.Utils;
using System.Linq;

namespace ShopifyBackendChallenge.Data.SqlServer
{
    public class SqlUserData : IUserData
    {
        private readonly RepoDbContext _dbContext;

        public SqlUserData(RepoDbContext context)
        {
            _dbContext = context;
        }

        public async Task<UserModel> AddUserAsync(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            return user;
        }

        public async Task<UserModel> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (PasswordUtil.VerifyPassword(password, user.Hash, user.Salt))
            {
                return user;
            }
            return null ;

        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public UserModel GetUserById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}