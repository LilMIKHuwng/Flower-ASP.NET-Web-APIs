using Flower.Contract.Services.Interface;
using Flower.ModelViews.UserModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        ///     Register a new user
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromForm] UserRegisterModelView model)
        {
            try
            {
                var result = await _userService.RegisterUser(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }
        /// <summary>
        ///     Update user profile
        /// </summary>
        [HttpPut("update-profile")]
        public async Task<ActionResult<string>> UpdateProfile([FromForm] UserUpdateProfileModelView model)
        {
            try
            {
                var result = await _userService.UpdateUserProfile(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Login
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Register([FromBody] UserLoginModelView model)
        {
            try
            {
                var result = await _userService.UserLogin(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }
        [HttpPost("confirm-user-register")]
        public async Task<ActionResult<string>> ConfirmUserRegister([FromBody] UserConfirmRegisterModelView model)
        {
            try
            {
                var result = await _userService.ConfirmUserRegister(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] UserForgotPasswordModelView model)
        {
            try
            {
                var result = await _userService.ForgotPassword(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }
        [HttpPost("reset-password")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] UserResetPasswordModelView model)
        {
            try
            {
                var result = await _userService.ResetPassword(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
