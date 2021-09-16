using Microsoft.AspNetCore.Mvc;
using ShopifyBackendChallenge.Core.Image;
using ShopifyBackendChallenge.Core.User;
using ShopifyBackendChallenge.Data.Common;
using ShopifyBackendChallenge.Web.Helpers;
using ShopifyBackendChallenge.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepo _imageRepo;
        private readonly IImageMetadata _imageMetadata;

        public ImagesController(IImageRepo imageRepo, IImageMetadata imageMetadata)
        {
            _imageRepo = imageRepo;
            _imageMetadata = imageMetadata;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostImage([FromForm] SingleImageModel model)
        {
            int userId = ((UserModel)HttpContext.Items["User"]).Id;
            string imagePath =  await _imageRepo.AddImageAsync(model.Image, userId);
            ImageModel imageMetadata = new ImageModel
            {
                Title = model.Title,
                Description = model.Description,
                ImageUri = imagePath,
                UserId = userId
            };
            await _imageMetadata.AddImageMetadataAsync(imageMetadata);
            await _imageMetadata.CommitAsync();
            return Ok(new { message = "Image Added Successfully" });
        }

        [Authorize]
        [HttpGet("metadata")]
        public async Task<IActionResult> GetAllUserImageMetadata()
        {
            int userId = ((UserModel)HttpContext.Items["User"]).Id;
            var images = await _imageMetadata.GetImagesMetadataByUserIdAsync(userId);
            return Ok(new {result = images });
        }
    }
}
