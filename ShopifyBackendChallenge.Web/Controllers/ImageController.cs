using Microsoft.AspNetCore.Mvc;
using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Data.Common;
using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ShopifyBackendChallenge.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepo _imageRepo;
        private readonly IImageMetadata _imageMetadata;
        private readonly IMapper _mapper;

        public ImagesController(IImageRepo imageRepo, IImageMetadata imageMetadata, IMapper mapper)
        {
            _imageRepo = imageRepo;
            _imageMetadata = imageMetadata;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds an image to the user's repository
        /// </summary>
        /// <response code="400">User provided an invalid request</response>
        /// <response code="401">User did not provide a valid JWT token</response>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostImage([FromForm] ImageCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                dto.UserId = ((UserReadDto)HttpContext.Items["User"]).Id;
                ImageModel imageModel = _mapper.Map<ImageModel>(dto);
                string imagePath = await _imageRepo.AddImageAsync(imageModel);

                var metadataModel = _mapper.Map<MetadataModel>(dto);
                metadataModel.ImageUri = imagePath;

                await _imageMetadata.AddImageMetadataAsync(metadataModel);
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
            int userId = ((UserReadDto)HttpContext.Items["User"]).Id;
            var metadata = await _imageMetadata.GetImagesMetadataByUserIdAsync(userId);

            return Ok(new {result = _mapper.Map<IEnumerable<ImageReadDto>>(metadata) });
        }

        /// <summary>
        /// Retrieves all public image metadata for the user by Tag
        /// </summary>
        /// <response code="401">User did not provide a valid JWT token</response>
        [Authorize]
        [HttpGet]
        [Route("search-global-metadata")]
        public async Task<IActionResult> GetAllUserImageMetadataContainingTag([FromQuery] string tag)
        {
            int userId = ((UserReadDto)HttpContext.Items["User"]).Id;
            var metadata = await _imageMetadata.GetImageMetadataByTagsAsync(tag, userId);
            return Ok(new { result = _mapper.Map<IEnumerable<ImageReadDto>>(metadata) });
        }

        /// <summary>
        /// Retrieves all personal images for the user
        /// </summary>
        /// <response code="401">User did not provide a valid JWT token</response>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUserImages()
        {
            int userId = ((UserReadDto)HttpContext.Items["User"]).Id;
            var metadata = await _imageMetadata.GetImagesMetadataByUserIdAsync(userId);
            var images = _imageRepo.GetImagesFromMetadataAsync(userId, metadata);
           
            return Ok(new { result = images});
        }
    }
}
