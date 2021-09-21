using Microsoft.AspNetCore.Http;
using ShopifyBackendChallenge.Web.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopifyBackendChallenge.Tests.Image
{
    public class ImageTests
    {
        [Fact]
        public void Image_NewModelValidProperties_Valid()
        {
            var content = "Hello World from a Fake Image";
            var fileName = "test.jpg";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile formData = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            ImageCreateDto image = new ImageCreateDto
            {
                ImageData = formData,
                Title = "Test Image",
                Description = "Test Image",
                Tags = new List<string> { "Dan", "Sheridan"}
            };

            ValidationContext context = new ValidationContext(image);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(image, context, results, true);
            Assert.True(validUser);
        }

        [Fact]
        public void Image_IncorrectExtension_Invalid()
        {
            var content = "Hello World from a Fake Image";
            var fileName = "test.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile formData = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            ImageCreateDto image = new ImageCreateDto
            {
                ImageData = formData,
                Title = "Test Image",
                Description = "Test Image",
                Tags = new List<string> { "Dan", "Sheridan" }
            };

            ValidationContext context = new ValidationContext(image);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(image, context, results, true);
            Assert.False(validUser);
        }

        [Fact]
        public void Image_MissingImageData_Invalid()
        {
            ImageCreateDto image = new ImageCreateDto
            {
                ImageData = null,
                Title = "Test Image",
                Description = "Test Image",
                Tags = new List<string> { "Dan", "Sheridan" }
            };

            ValidationContext context = new ValidationContext(image);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(image, context, results, true);
            Assert.False(validUser);
        }

        [Fact]
        public void Image_NewModelTitleTooLong_Invalid()
        {
            var content = "Hello World from a Fake Image";
            var fileName = "test.jpg";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile formData = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            ImageCreateDto image = new ImageCreateDto
            {
                ImageData = formData,
                Title = new string('c', 101),
                Description = "Test Image",
                Tags = new List<string> { "Dan", "Sheridan" }
            };

            ValidationContext context = new ValidationContext(image);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(image, context, results, true);
            Assert.False(validUser);
        }

        [Fact]
        public void Image_MissingTitle_Invalid()
        {
            var content = "Hello World from a Fake Image";
            var fileName = "test.jpg";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile formData = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            ImageCreateDto image = new ImageCreateDto
            {
                ImageData = formData,
                Title = null,
                Description = "Test Image",
                Tags = new List<string> { "Dan", "Sheridan" }
            };

            ValidationContext context = new ValidationContext(image);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(image, context, results, true);
            Assert.False(validUser);
        }

        [Fact]
        public void Image_NewModelDescriptionTooLong_Invalid()
        {
            var content = "Hello World from a Fake Image";
            var fileName = "test.jpg";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile formData = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            ImageCreateDto image = new ImageCreateDto
            {
                ImageData = formData,
                Title = "Test Image",
                Description = new string('c', 201),
                Tags = new List<string> { "Dan", "Sheridan" }
            };

            ValidationContext context = new ValidationContext(image);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(image, context, results, true);
            Assert.False(validUser);
        }

        [Fact]
        public void Image_MissingDescription_Invalid()
        {
            var content = "Hello World from a Fake Image";
            var fileName = "test.jpg";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile formData = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            ImageCreateDto image = new ImageCreateDto
            {
                ImageData = formData,
                Title = "Test Image",
                Description = null,
                Tags = new List<string> { "Dan", "Sheridan" }
            };

            ValidationContext context = new ValidationContext(image);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(image, context, results, true);
            Assert.False(validUser);
        }

        [Fact]
        public void Image_MissingTags_Invalid()
        {
            var content = "Hello World from a Fake Image";
            var fileName = "test.jpg";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile formData = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            ImageCreateDto image = new ImageCreateDto
            {
                ImageData = formData,
                Title = "Test Image",
                Description = null,
                Tags = null
            };

            ValidationContext context = new ValidationContext(image);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(image, context, results, true);
            Assert.False(validUser);
        }

        [Fact]
        public void Image_EmptyList_Invalid()
        {
            var content = "Hello World from a Fake Image";
            var fileName = "test.jpg";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile formData = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            ImageCreateDto image = new ImageCreateDto
            {
                ImageData = formData,
                Title = "Test Image",
                Description = null,
                Tags = new List<string> { }
            };

            ValidationContext context = new ValidationContext(image);
            List<ValidationResult> results = new List<ValidationResult>();
            bool validUser = Validator.TryValidateObject(image, context, results, true);
            Assert.False(validUser);
        }
    }
}
