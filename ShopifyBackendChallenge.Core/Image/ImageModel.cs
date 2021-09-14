using System;
using System.Collections.Generic;
using System.Text;

namespace ShopifyBackendChallenge.Core.Image
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUri { get; set; }
        public IEnumerable<bool> Hash { get; }
        public string Description { get; set; }
    }
}
