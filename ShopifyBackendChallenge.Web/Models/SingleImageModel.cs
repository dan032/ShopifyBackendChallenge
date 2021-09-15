using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Models
{
    public class SingleImageModel
    {
        public IFormFile Image { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
    }
}
