using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.API.Models;
using PMS.API.Models.Dto;
using PMS.Service.Services;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            var result = await _userService.Authenticate(userObj);
            //If username and password correct
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            var result = await _userService.RegisterUser(userObj);

            return Ok(new
            {
                Message = "User Registered!",
                Data = result
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        //Give new access token
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenApiDto tokenApiDto)
        {
            var result = await _userService.Refresh(tokenApiDto);

            return Ok(result);
        }

        //Send reset password email
        [HttpPost("send-reset-password/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var result = await _userService.SendEmail(email);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Email Sent!",
                Data = result
            });
        }

        //Reset password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var result = await _userService.ResetPassword(resetPasswordDto);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Password Reset Successfully",
                Data = result
            });
        }

    }
}
