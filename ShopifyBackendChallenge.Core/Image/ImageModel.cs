using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ShopifyBackendChallenge.Core.Image
{
    public class ImageModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }

        [JsonIgnore]
        public string ImageUri { get; set; }

        [JsonIgnore]
        public IEnumerable<bool> Hash { get; }
        public string Description { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
    }
}
