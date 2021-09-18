using ShopifyBackendChallenge.Core.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace ShopifyBackendChallenge.Tests.User
{
    public class UserTests
    {
        [Fact]
        public void User_NewUserValidProperties_Valid()
        {
            UserModel user = new UserModel
            {
                Username = "danbutts",
                Hash = "hash-goes-here",
                Salt = "salt-goes-here"
            };

            ValidationContext context = new ValidationContext(user);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(user, context, results, true);
            Assert.True(validUser);
        }

        [Fact]
        public void User_NewUserUsernameTooLong_Invalid()
        {
            UserModel user = new UserModel
            {
                Username = "danbutasdasdasdadxasfdsddsafsdjklasdfnkohnoksfdanofsadnkfsdanksdfnkjfdsalndslfaknlkdfsnjdfts",
                Hash = "hash-goes-here",
                Salt = "salt-goes-here"
            };

            ValidationContext context = new ValidationContext(user);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(user, context, results, true);
            Assert.False(validUser);
        }

        [Fact]
        public void User_NewUserMissingProperties_Invalid()
        {
            UserModel user = new UserModel();

            ValidationContext context = new ValidationContext(user);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(user, context, results, true);
            Assert.False(validUser);
        }
    }
}
