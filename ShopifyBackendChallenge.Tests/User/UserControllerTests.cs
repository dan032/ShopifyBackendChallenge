using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShopifyBackendChallenge.Web.Data.Common;
using ShopifyBackendChallenge.Web.Data.SqlServer;
using ShopifyBackendChallenge.Tests.Utils;
using ShopifyBackendChallenge.Web.Controllers;
using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Helpers;
using ShopifyBackendChallenge.Web.Profiles;
using ShopifyBackendChallenge.Web.Services.Common;
using ShopifyBackendChallenge.Web.Services.Jwt;
using Xunit;

namespace ShopifyBackendChallenge.Tests.User
{
    public class UserControllerTests : IClassFixture<SharedDatabaseFixture>
    {
        public UserControllerTests(SharedDatabaseFixture fixture) => Fixture = fixture;

        public SharedDatabaseFixture Fixture { get; }

        [Fact]
        public async void UserController_Registration_Successful()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    IUserData userData = new SqlUserData(context);
                    var appSettings = Options.Create(new AppSettings
                    {
                        Secret = "Daasdfsadadssadadsasddasasfdffsadn"
                    });

                    var mockMapper = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new ImagesProfile());
                        cfg.AddProfile(new MetadataProfile());
                        cfg.AddProfile(new UsersProfile());
                    });
                    IMapper mapper = mockMapper.CreateMapper();

                    IUserAuthentication userAuthentication = new JwtUserAuthentication(userData, appSettings, mapper);
                    var controller = new UserController(userAuthentication, userData, mapper);
                    UserCreateDto authenticateRequest = new UserCreateDto
                    {
                        Username = "test2",
                        Password = "test2"
                    };

                    var actionResult = await controller.Register(authenticateRequest);
                    var okResult = actionResult as OkObjectResult;

                    Assert.Equal(200, okResult.StatusCode);
                }
            }
        }

        [Fact]
        public async void UserController_DuplicateRegistration_Fail()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    IUserData userData = new SqlUserData(context);
                    var appSettings = Options.Create(new AppSettings
                    {
                        Secret = "Daasdfsadadssadadsasddasasfdffsadn"
                    });

                    var mockMapper = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new ImagesProfile());
                        cfg.AddProfile(new MetadataProfile());
                        cfg.AddProfile(new UsersProfile());
                    });
                    IMapper mapper = mockMapper.CreateMapper();

                    IUserAuthentication userAuthentication = new JwtUserAuthentication(userData, appSettings, mapper);
                    var controller = new UserController(userAuthentication, userData, mapper);
                    UserCreateDto authenticateRequest = new UserCreateDto
                    {
                        Username = "test2",
                        Password = "test2"
                    };

                    await controller.Register(authenticateRequest);
                    var actionResult = await controller.Register(authenticateRequest);
                    var result = actionResult as BadRequestObjectResult;

                    Assert.Equal(400, result.StatusCode);
                }
            }
        }

        [Fact]
        public async void UserController_AuthenticationCorrectCredentials_Success()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    IUserData userData = new SqlUserData(context);
                    var appSettings = Options.Create(new AppSettings
                    {
                        Secret = "Daasdfsadadssadadsasddasasfdffsadn"
                    });

                    var mockMapper = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new ImagesProfile());
                        cfg.AddProfile(new MetadataProfile());
                        cfg.AddProfile(new UsersProfile());
                    });
                    IMapper mapper = mockMapper.CreateMapper();

                    IUserAuthentication userAuthentication = new JwtUserAuthentication(userData, appSettings, mapper);
                    var controller = new UserController(userAuthentication, userData, mapper);
                    UserCreateDto authenticateRequest = new UserCreateDto
                    {
                        Username = "test",
                        Password = "test"
                    };

                    var actionResult = await controller.Authenticate(authenticateRequest);
                    var result = actionResult as OkObjectResult;

                    Assert.Equal(200, result.StatusCode);
                }
            }
        }
        [Fact]
        public async void UserController_AuthenticationIncorrectCredentials_Failure()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    IUserData userData = new SqlUserData(context);
                    var appSettings = Options.Create(new AppSettings
                    {
                        Secret = "Daasdfsadadssadadsasddasasfdffsadn"
                    });


                    var mockMapper = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new ImagesProfile());
                        cfg.AddProfile(new MetadataProfile());
                        cfg.AddProfile(new UsersProfile());
                    });
                    IMapper mapper = mockMapper.CreateMapper();

                    IUserAuthentication userAuthentication = new JwtUserAuthentication(userData, appSettings, mapper);
                    var controller = new UserController(userAuthentication, userData, mapper);
                    UserCreateDto authenticateRequest = new UserCreateDto
                    {
                        Username = "test",
                        Password = "wrong"
                    };

                    var actionResult = await controller.Authenticate(authenticateRequest);
                    var result = actionResult as BadRequestObjectResult;

                    Assert.Equal(400, result.StatusCode);
                }
            }
        }
    }
}
