using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Models;
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
            UserCreateDto user = new UserCreateDto
            {
                Username = "danbutts",
                Password = "password"
            };

            ValidationContext context = new ValidationContext(user);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(user, context, results, true);
            Assert.True(validUser);
        }

        [Fact]
        public void User_NewUserUsernameTooLong_Invalid()
        {
            UserCreateDto user = new UserCreateDto
            {
                Username = "danbutasdasdasdadxasfdsddsafsdjklasdfnkohnoksfdanofsadnkfsdanksdfnkjfdsalndslfaknlkdfsnjdfts",
                Password = "password"
            };

            ValidationContext context = new ValidationContext(user);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(user, context, results, true);
            Assert.False(validUser);
        }

        [Fact]
        public void User_NewUserMissingProperties_Invalid()
        {
            UserCreateDto user = new UserCreateDto();

            ValidationContext context = new ValidationContext(user);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(user, context, results, true);
            Assert.False(validUser);
        }
    }
}
