using System.ComponentModel.DataAnnotations;


namespace ShopifyBackendChallenge.Web.Dtos
{
    public class UserCreateDto
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
