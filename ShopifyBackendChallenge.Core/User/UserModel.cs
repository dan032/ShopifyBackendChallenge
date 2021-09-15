using Microsoft.AspNetCore.Identity;
using ShopifyBackendChallenge.Core.Image;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopifyBackendChallenge.Core.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public IEnumerable<ImageModel> Images { get; set; }
    }
}
