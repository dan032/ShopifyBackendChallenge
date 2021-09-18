using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopifyBackendChallenge.Core.User;
using ShopifyBackendChallenge.Data;
using ShopifyBackendChallenge.Data.Common;
using ShopifyBackendChallenge.Data.FileStorage;
using ShopifyBackendChallenge.Data.SqlServer;
using ShopifyBackendChallenge.Data.Utils;
using ShopifyBackendChallenge.Tests.Utils;
using ShopifyBackendChallenge.Web.Controllers;
using ShopifyBackendChallenge.Web.Helpers;
using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Services.common;
using ShopifyBackendChallenge.Web.Services.Jwt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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
                    var controller = new ImagesController(imageRepo, imageMetadata);

                    var image = File.OpenRead(@"..\..\..\TestImages\test.jpg");

                    var formData = new FormFile(image, 0, 0, "test", "test.jpg");


                    SingleImageModel singleImageModel = new SingleImageModel
                    {
                        Image = formData,
                        Description = "Test",
                        Title = "Test",
                        Tags = new List<string>() { "Dan"}
                    };
                    controller.ControllerContext.HttpContext = new DefaultHttpContext();
                    controller.HttpContext.Items["User"] = new UserModel
                    {
                        Id = 1,
                        Username = "test",
                        Hash = "hash",
                        Salt = "salt"
                    };

                    IActionResult actionResult = await controller.PostImage(singleImageModel);
                    var okResult = actionResult as OkObjectResult;
                    Assert.Equal(200, okResult.StatusCode);
                }
            }
        }
    }
}
