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
    public class ImageController : ControllerBase
    {
        private readonly IImageRepo _imageRepo;
        private readonly IImageMetadata _imageMetadata;

        public ImageController(IImageRepo imageRepo, IImageMetadata imageMetadata)
        {
            _imageRepo = imageRepo;
            _imageMetadata = imageMetadata;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostImage([FromForm] SingleImageModel model)
        {
            UserModel user = (UserModel)HttpContext.Items["User"];
            string imagePath =  await _imageRepo.AddImageAsync(model.Image, user.Id);
            ImageModel imageMetadata = new ImageModel
            {
                Title = model.Title,
                Description = model.Description,
                ImageUri = imagePath
            };
            await _imageMetadata.AddImageMetadataAsync(imageMetadata);
            await _imageMetadata.CommitAsync();
            return Ok(new { message = "Image Added Successfully" });
        }
    }
}
