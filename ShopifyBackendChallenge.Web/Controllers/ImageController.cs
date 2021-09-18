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

        /// <summary>
        /// Adds an image to the user's repository
        /// </summary>
        /// <response code="400">User provided an invalid request</response>
        /// <response code="401">User did not provide a valid JWT token</response>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostImage([FromForm] SingleImageModel model)
        {
            if (ModelState.IsValid)
            {
                int userId = ((UserModel)HttpContext.Items["User"]).Id;
                string imagePath = await _imageRepo.AddImageAsync(model.Image, userId);
                ImageModel imageMetadata = new ImageModel
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUri = imagePath,
                    UserId = userId,
                    Tags = String.Join(",", model.Tags)
                };

                await _imageMetadata.AddImageMetadataAsync(imageMetadata);
                await _imageMetadata.CommitAsync();
                return Ok(new { message = "Image Added Successfully" });
            }
            return BadRequest(new { message = "Invalid Request" });
        }

        /// <summary>
        /// Retrieves all image metadata for the user
        /// </summary>
        /// <response code="401">User did not provide a valid JWT token</response>
        [Authorize]
        [HttpGet("personal-metadata")]
        public async Task<IActionResult> GetAllUserImageMetadata()
        {
            int userId = ((UserModel)HttpContext.Items["User"]).Id;
            var metadata = await _imageMetadata.GetImagesMetadataByUserIdAsync(userId);
            return Ok(new {result = metadata });
        }

        /// <summary>
        /// Retrieves all image metadata for the user
        /// </summary>
        /// <response code="401">User did not provide a valid JWT token</response>
        [Authorize]
        [HttpGet]
        [Route("search-global-metadata")]
        public async Task<IActionResult> GetAllUserImageMetadataContainingTag([FromQuery] string tag)
        {
            int userId = ((UserModel)HttpContext.Items["User"]).Id;
            var metadata = await _imageMetadata.GetImageMetadataByTagsAsync(tag, userId);
            return Ok(new { result = metadata });
        }

        /// <summary>
        /// Retrieves all images for the user
        /// </summary>
        /// <response code="401">User did not provide a valid JWT token</response>
        [Authorize]
        [HttpGet]
        public IActionResult GetAllUserImages()
        {
            int userId = ((UserModel)HttpContext.Items["User"]).Id;
            var images = _imageRepo.GetImagesByUserIdAsync(userId);
            return Ok(new { result = images });
        }
    }
}
