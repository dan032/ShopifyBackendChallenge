using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopifyBackendChallenge.Core.User;
using ShopifyBackendChallenge.Web.Helpers;
using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Services.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserAuthentication _userAuthentication;

        public UserController(IUserAuthentication userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            AuthenticateResponse response = await _userAuthentication.Authenticate(model);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(response);
        }
    }
}
