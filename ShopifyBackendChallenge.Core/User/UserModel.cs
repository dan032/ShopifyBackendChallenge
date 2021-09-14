using Microsoft.AspNetCore.Identity;
using ShopifyBackendChallenge.Core.Image;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopifyBackendChallenge.Core.User
{
    public class UserModel : IdentityUser
    {
        public IEnumerable<ImageModel> Images { get; set; }
    }
}
