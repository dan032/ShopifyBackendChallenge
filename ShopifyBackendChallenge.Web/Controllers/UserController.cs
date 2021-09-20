using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopifyBackendChallenge.Web.Data.Common;
using ShopifyBackendChallenge.Web.Dtos;
using ShopifyBackendChallenge.Web.Models;
using ShopifyBackendChallenge.Web.Services.Common;
using ShopifyBackendChallenge.Web.Utils;
using System.Threading.Tasks;

namespace ShopifyBackendChallenge.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAuthentication _userAuthentication;
        private readonly IUserData _userData;
        private readonly IMapper _mapper;

        public UserController(IUserAuthentication userAuthentication, IUserData userData, IMapper mapper)
        {
            _userAuthentication = userAuthentication;
            _userData = userData;
            _mapper = mapper;
        }

        /// <summary>
        /// Provides the user with their JWT token when they provide valid credentials
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Provides the user with their JWT token</response>
        /// <response code="400">User provided invalid credentials</response>
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserCreateDto model)
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
        public async Task<IActionResult> Register(UserCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var preUser = _mapper.Map<UserModel>(model);
                HashSalt hashSalt = PasswordUtil.GenerateSaltedHash(model.Password);
                preUser.Hash = hashSalt.Hash;
                preUser.Salt = hashSalt.Salt;
                UserModel user = await _userData.AddUserAsync(preUser);
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
