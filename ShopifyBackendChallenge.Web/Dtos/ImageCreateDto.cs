using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ShopifyBackendChallenge.Web.Dtos
{
    public class ImageCreateDto
    {
        [Required]
        public IFormFile ImageData { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public IEnumerable<string> Tags { get; set; }

        [Required]
        public bool Private { get; set; }

        public int UserId { get; set; }

        public string ImageUri { get; set; }
    }
}
