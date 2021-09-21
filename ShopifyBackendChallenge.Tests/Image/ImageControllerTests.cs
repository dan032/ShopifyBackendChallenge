using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShopifyBackendChallenge.Web.Data.Common;
using ShopifyBackendChallenge.Web.Data.FileStorage;
using ShopifyBackendChallenge.Web.Data.SqlServer;
using ShopifyBackendChallenge.Web.Controllers;
using ShopifyBackendChallenge.Web.Helpers;
using System.Collections.Generic;
using System.IO;
using Xunit;
using AutoMapper;
using ShopifyBackendChallenge.Web.Profiles;
using ShopifyBackendChallenge.Web.Dtos;
using Moq;
using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Utils;
using System;

namespace ShopifyBackendChallenge.Tests.Image
{
    public class ImageControllerTests
    {
        [Fact]
        public async void ImageController_PostImage_Successful()
        {
            var appSettings = Options.Create(new AppSettings
            {
                Secret = "Daasdfsadadssadadsasddasasfdffsadn"
            });

            HashSalt hashSalt = PasswordUtil.GenerateSaltedHash("test");
            var fakeUser = new UserModel
            {
                Id = 1,
                Username = "test",
                Hash = hashSalt.Hash,
                Salt = hashSalt.Salt
            };

            //Setup mock file using a memory stream
            var content = "Hello World from a Fake Image";
            var fileName = "test.jpg";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile formData = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);


            ImageCreateDto dto = new ImageCreateDto
            {
                ImageData = formData,
                Description = "Test",
                Title = "Test",
                Tags = new List<string>() { "Dan" },
                Private = false
            };

            var mockUserData = new Mock<IUserData>();
            mockUserData.Setup(r => r.GetUser(It.IsAny<string>(), It.IsAny<UserModel>())).ReturnsAsync(GetTestUser());

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ImagesProfile());
                cfg.AddProfile(new MetadataProfile());
                cfg.AddProfile(new UsersProfile());
            });

            IMapper mapper = mockMapper.CreateMapper();

            var fakeImageRepo = new Mock<IImageRepo>();
            fakeImageRepo.Setup(r => r.AddImageAsync(It.IsAny<ImageModel>())).ReturnsAsync(GetTestImageUri());

            var mockMetaData = new Mock<IImageMetadata>();
            mockMetaData.Setup(r => r.AddImageMetadataAsync(It.IsAny<MetadataModel>())).ReturnsAsync(GetFakeMetaData(mapper, dto));


            var controller = new ImagesController(fakeImageRepo.Object, mockMetaData.Object, mapper);

            UserReadDto userReadDto = mapper.Map<UserReadDto>(fakeUser);

            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Items["User"] = userReadDto;

            IActionResult actionResult = await controller.PostImage(dto);
            var okResult = actionResult as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);
        }

        private MetadataModel GetFakeMetaData(IMapper mapper, ImageCreateDto dto)
        {
            ImageModel image = mapper.Map<ImageModel>(dto);
            image.UserId = 1;

            var fakeImageMetaData = mapper.Map<MetadataModel>(dto);
            fakeImageMetaData.UserId = 1;
            fakeImageMetaData.ImageUri = "/2/saa54l.jph";

            return fakeImageMetaData;
        }

        private string GetTestImageUri()
        {
            return "/2/saa54l.jph";
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
    }
}
