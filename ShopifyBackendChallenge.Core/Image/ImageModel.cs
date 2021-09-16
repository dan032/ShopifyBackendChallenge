using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace ShopifyBackendChallenge.Core.Image
{
    public class ImageModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [JsonIgnore]
        [Required]
        public string ImageUri { get; set; }

        [JsonIgnore]
        public IEnumerable<bool> Hash { get; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [JsonIgnore]
        [Required]
        public int UserId { get; set; }
    }
}
