using Microsoft.AspNetCore.Http;

namespace ShopifyBackendChallenge.Web.Dtos
{
    public class ImageReadDto
    {
        public byte[] ImageData { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public bool Private { get; set; }
        public int UserId { get; set; }
    }
}
