using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopifyBackendChallenge.Web.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        public string Hash { get; set; }

        public string Salt { get; set; }
    }
}
