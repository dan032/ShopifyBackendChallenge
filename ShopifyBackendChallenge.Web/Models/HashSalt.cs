using Microsoft.EntityFrameworkCore;

namespace ShopifyBackendChallenge.Web.Models
{
    [Keyless]
    public class HashSalt
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
