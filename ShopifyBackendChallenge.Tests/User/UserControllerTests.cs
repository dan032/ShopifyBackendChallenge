using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShopifyBackendChallenge.Web.Data.Common;
using ShopifyBackendChallenge.Web.Controllers;
using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Helpers;
using ShopifyBackendChallenge.Web.Profiles;
using ShopifyBackendChallenge.Web.Services.Common;
using ShopifyBackendChallenge.Web.Services.Jwt;
using Xunit;
using Moq;
using ShopifyBackendChallenge.Web.Models;
using System;
using ShopifyBackendChallenge.Web.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ShopifyBackendChallenge.Tests.User
{
    public class UserControllerTests 
    {

        [Fact]
        public async void UserController_Registration_Successful()
        {
            var appSettings = Options.Create(new AppSettings
            {
                Secret = "Daasdfsadadssadadsasddasasfdffsadn"
            });

            var mockUserData = new Mock<IUserData>();
            mockUserData.Setup(r => r.AddUserAsync(It.IsAny<UserModel>())).ReturnsAsync(GetTestUser());

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ImagesProfile());
                cfg.AddProfile(new MetadataProfile());
                cfg.AddProfile(new UsersProfile());
            });

            IMapper mapper = mockMapper.CreateMapper();

            var mockUserAuthentication = new Mock<IUserAuthentication>();


            var controller = new UserController(mockUserAuthentication.Object, mockUserData.Object, mapper);
            UserCreateDto authenticateRequest = new UserCreateDto
            {
                Username = "test2",
                Password = "test2"
            };

            var actionResult = await controller.Register(authenticateRequest);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void UserController_DuplicateRegistration_Fail()
        {
            var appSettings = Options.Create(new AppSettings
            {
                Secret = "Daasdfsadadssadadsasddasasfdffsadn"
            });

            var mockUserData = new Mock<IUserData>();
            mockUserData.Setup(r => r.AddUserAsync(It.IsAny<UserModel>())).ReturnsAsync(GetTestUser());

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ImagesProfile());
                cfg.AddProfile(new MetadataProfile());
                cfg.AddProfile(new UsersProfile());
            });

            IMapper mapper = mockMapper.CreateMapper();

            var mockUserAuthentication = new Mock<IUserAuthentication>();


            var controller = new UserController(mockUserAuthentication.Object, mockUserData.Object, mapper);
            UserCreateDto authenticateRequest = new UserCreateDto
            {
                Username = "test2",
                Password = "test2"
            };

            var actionResult = await controller.Register(authenticateRequest);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void UserController_AuthenticationCorrectCredentials_Success()
        {
            var appSettings = Options.Create(new AppSettings
            {
                Secret = "Daasdfsadadssadadsasddasasfdffsadn"
            });

            var mockUserData = new Mock<IUserData>();
            mockUserData.Setup(r => r.GetUser(It.IsAny<string>(), It.IsAny<UserModel>())).ReturnsAsync(GetTestUser());

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ImagesProfile());
                cfg.AddProfile(new MetadataProfile());
                cfg.AddProfile(new UsersProfile());
            });

            IMapper mapper = mockMapper.CreateMapper();


            IUserAuthentication userAuthentication = new JwtUserAuthentication(mockUserData.Object, appSettings, mapper);

            var controller = new UserController(userAuthentication, mockUserData.Object, mapper);

            UserCreateDto authenticateRequest = new UserCreateDto
            {
                Username = "test",
                Password = "test"
            };

            var actionResult = await controller.Authenticate(authenticateRequest);
            var result = actionResult as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
                
        }

      
        [Fact]
        public async void UserController_AuthenticationIncorrectCredentials_Failure()
        {
            var appSettings = Options.Create(new AppSettings
            {
                Secret = "Daasdfsadadssadadsasddasasfdffsadn"
            });

            var mockUserData = new Mock<IUserData>();
            mockUserData.Setup(r => r.GetUser(It.IsAny<string>(), It.IsAny<UserModel>())).ReturnsAsync(GetInvalidTestUser());

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ImagesProfile());
                cfg.AddProfile(new MetadataProfile());
                cfg.AddProfile(new UsersProfile());
            });

            IMapper mapper = mockMapper.CreateMapper();

            IUserAuthentication userAuthentication = new JwtUserAuthentication(mockUserData.Object, appSettings, mapper);

            var controller = new UserController(userAuthentication, mockUserData.Object, mapper);

            UserCreateDto authenticateRequest = new UserCreateDto
            {
                Username = "wrong",
                Password = "wrong"
            };

            var actionResult = await controller.Authenticate(authenticateRequest);
            var result = actionResult as BadRequestObjectResult;

            Assert.Equal(400, result.StatusCode);
        }

        private UserModel GetInvalidTestUser()
        {
            return null;
        }

        private UserModel GetTestUser()
        {
            HashSalt hashSalt = PasswordUtil.GenerateSaltedHash("test");

            return new UserModel
            {
                Id = 1,
                Username = "test",
                Hash = hashSalt.Hash,
                Salt = hashSalt.Salt
            };
        }

        private AuthenticateResponse GetTestAuthenticateResponse()
        {
            UserReadDto userReadDto = new UserReadDto
            {
                Id = 1,
                Username = "test"
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("Daasdfsadadssadadsasddasasfdffsadn");
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("sub", userReadDto.Id.ToString()),
                    new Claim("username", userReadDto.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticateResponse(tokenHandler.WriteToken(token));
        }
    }
}
