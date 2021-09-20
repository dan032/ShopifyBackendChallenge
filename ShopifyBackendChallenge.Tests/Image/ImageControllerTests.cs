using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShopifyBackendChallenge.Web.Data.Common;
using ShopifyBackendChallenge.Web.Data.FileStorage;
using ShopifyBackendChallenge.Web.Data.SqlServer;
using ShopifyBackendChallenge.Tests.Utils;
using ShopifyBackendChallenge.Web.Controllers;
using ShopifyBackendChallenge.Web.Helpers;
using System.Collections.Generic;
using System.IO;
using Xunit;
using AutoMapper;
using ShopifyBackendChallenge.Web.Profiles;
using ShopifyBackendChallenge.Web.Dtos;

namespace ShopifyBackendChallenge.Tests.Image
{
    public class ImageControllerTests : IClassFixture<SharedDatabaseFixture>
    {
        public ImageControllerTests(SharedDatabaseFixture fixture) => Fixture = fixture;

        public SharedDatabaseFixture Fixture { get; }

        [Fact]
        public async void ImageController_UploadImage_Successful()
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

                    IImageMetadata imageMetadata = new SqlImageMetadata(context);
                    IImageRepo imageRepo = new FolderStorageImageRepo();
                    var mockMapper = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new ImagesProfile());
                        cfg.AddProfile(new MetadataProfile());
                        cfg.AddProfile(new UsersProfile());
                    });
                    IMapper mapper = mockMapper.CreateMapper();
                    var controller = new ImagesController(imageRepo, imageMetadata, mapper);

                    var image = File.OpenRead(@"..\..\..\TestImages\test.jpg");

                    var formData = new FormFile(image, 0, 0, "test", "test.jpg");


                    ImageCreateDto singleImageModel = new ImageCreateDto
                    {
                        ImageData = formData,
                        Description = "Test",
                        Title = "Test",
                        Tags = new List<string>() { "Dan" },
                        Private = false
                    };

                    controller.ControllerContext.HttpContext = new DefaultHttpContext();
                    controller.HttpContext.Items["User"] = new UserCreateDto
                    {
                        Username = "test",
                        Password = "test"
                    };

                    IActionResult actionResult = await controller.PostImage(singleImageModel);
                    var okResult = actionResult as OkObjectResult;
                    Assert.Equal(200, okResult.StatusCode);
                }
            }
        }
    }
}
