using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace ShopifyBackendChallenge.Web.Models
{
    public class ImageModel
    {
        [Required]
        public IFormFile ImageData { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string ImageUri { get; set; }
    }
}
