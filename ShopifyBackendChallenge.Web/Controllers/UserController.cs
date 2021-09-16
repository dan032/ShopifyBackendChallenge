using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopifyBackendChallenge.Core.User;
using ShopifyBackendChallenge.Data.Common;
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
        private IUserData _userData;

        public UserController(IUserAuthentication userAuthentication, IUserData userData)
        {
            _userAuthentication = userAuthentication;
            _userData = userData;
        }

        /// <summary>
        /// Provides the user with their JWT token when they provide valid credentials
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Provides the user with their JWT token</response>
        /// <response code="400">User provided invalid credentials</response>
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            if (ModelState.IsValid)
            {
                AuthenticateResponse response = await _userAuthentication.Authenticate(model);

                if (response != null)
                {
                    return Ok(response);
                }
            }
            return BadRequest(new { message = "Username or password is incorrect" });

        }

        /// <summary>
        /// Allows the user to register for the repository
        /// </summary>
        /// <param name="model"></param>
        /// <response code="400">User provided invalid credentials</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthenticateRequest model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = await _userData.AddUserAsync(model.Username, model.Password);
                if (user != null)
                {
                    await _userData.CommitAsync();
                    return Ok(new { message = "Registration Successful" });
                }
            }
            return BadRequest(new { message = "Invalid registration request" });
        }
    }
}
