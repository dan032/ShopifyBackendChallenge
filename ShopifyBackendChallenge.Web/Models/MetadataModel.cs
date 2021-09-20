using System.ComponentModel.DataAnnotations;

namespace ShopifyBackendChallenge.Web.Models
{
    public class MetadataModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string ImageUri { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Tags { get; set; }

        [Required]
        public bool Private { get; set; }
    }
}
