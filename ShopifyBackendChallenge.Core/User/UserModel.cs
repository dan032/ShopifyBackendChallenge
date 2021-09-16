using Microsoft.AspNetCore.Identity;
using ShopifyBackendChallenge.Core.Image;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopifyBackendChallenge.Core.User
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        public string Hash { get; set; }

        [Required]
        public string Salt { get; set; }
    }
}
