using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.API.Data;
using PMS.API.Helpers;
using PMS.API.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public UserController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            //If userObj is null
            if (userObj == null) 
            {
                return BadRequest();
            }

            //Check if user has the same username and password
            var user = await _authContext.Users.FirstOrDefaultAsync(x => x.Username == userObj.Username);

            //If user doesn't exist return
            if (user == null)
            {
                return NotFound(new { Message = "User Not Found" });
            }

            //verify password
            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is incorrect" });
            }

            //If username and password correct
            return Ok(new
            {
                Message = "Login Success!"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            //If userObj is null
            if (userObj == null)
            {
                return BadRequest();
            }
            //Check username
            if(await CheckUsernameExistAsync(userObj.Username))
                return BadRequest(new {Message = "Username Already Exist!"});

            //Check email
            if (await CheckEmailExistAsync(userObj.Email))
                return BadRequest(new { Message = "Email Already Exist!" });

            //Check password strength
            var pass = CheckPasswrodStrength(userObj.Password);
            if(!string.IsNullOrEmpty(pass))
                return BadRequest(new {Message = pass.ToString()});

            //Password hashing
            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Role = "User";
            userObj.Token = "";
            await _authContext.Users.AddAsync(userObj);
            await _authContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "User Registered!"
            });
        }

        private async Task<bool> CheckUsernameExistAsync(string username)
        {
            return await _authContext.Users.AnyAsync(x => x.Username == username);
        }

        private async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _authContext.Users.AnyAsync(x => x.Email == email);
        }

        private string CheckPasswrodStrength(string password)
        {
            StringBuilder sb = new StringBuilder();

            if(password.Length < 8)
            {
                sb.Append("Minimum password length should be 8"+Environment.NewLine);
            }

            if (!Regex.IsMatch(password, "[a-z]") || !Regex.IsMatch(password, "[A-Z]") || !Regex.IsMatch(password, "[0-9]"))
            {
                sb.Append("Password should have at least:\n-one small letter\n-one capital letter\n-one number" + Environment.NewLine);
            }
                
            if (!Regex.IsMatch(password, "[<,>,@,!]"))
            {
                sb.Append("Password should have at least one special character" + Environment.NewLine);
            }
                
            return sb.ToString();
        }
    }
}
