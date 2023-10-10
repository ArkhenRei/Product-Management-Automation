using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PMS.API.Data;
using PMS.API.Models;
using PMS.API.Models.Dto;
using PMS.API.UtilityService;
using PMS.API.Helpers;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PMS.Service.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _authContext;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public UserService(AppDbContext authContext, IConfiguration config, IEmailService emailService)
        {
            _authContext = authContext;
            _config = config;
            _emailService = emailService;

        }

        public async Task<TokenApiDto> Authenticate(User userObj)
        {
            //If userObj is null
            if (userObj == null)
            {
                throw new Exception("Bad Request");
            }
            //Check if user has the same username and password
            var user = await _authContext.Users.FirstOrDefaultAsync(x => x.Username == userObj.Username);
            //If user doesn't exist return
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            //verify password
            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                throw new Exception("Password is incorrect");
            }

            user.Token = CreateJwt(user);
            var newAccessToken = user.Token;
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpireTime = DateTime.Now.AddDays(5);
            await _authContext.SaveChangesAsync();

            return new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }

        public async Task<List<User>> GetAllUsers()
        {
            var user = await _authContext.Users.ToListAsync();
            return user;
        }

        public async Task<TokenApiDto> Refresh(TokenApiDto tokenApiDto)
        {
            if (tokenApiDto is null)
                throw new Exception("Invalid Client Request");

            string accessToken = tokenApiDto.AccessToken;
            string refreshToken = tokenApiDto.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;
            var user = await _authContext.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpireTime <= DateTime.Now)
                throw new Exception("Invalid Token");

            var newAccessToken = CreateJwt(user);
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _authContext.SaveChangesAsync();

            return new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }

        public async Task<User> RegisterUser(User userObj)
        {
            //If userObj is null
            if (userObj == null)
            {
                throw new Exception("Bad Request");
            }
            //Check username
            if (await CheckUsernameExistAsync(userObj.Username))
                throw new Exception("Username Already Exist!");
            //Check email
            if (await CheckEmailExistAsync(userObj.Email))
                throw new Exception("Email Already Exist!");
            //Check password strength
            var pass = CheckPasswrodStrength(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
                throw new Exception(pass.ToString());

            //Password hashing
            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Role = "User";
            userObj.Token = "";
            await _authContext.Users.AddAsync(userObj);
            await _authContext.SaveChangesAsync();

            return userObj;
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

            if (password.Length < 8)
            {
                sb.Append("Minimum password length should be 8" + Environment.NewLine);
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

        //Create JWT Token
        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("topsecretkey..........");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, $"{user.Username}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }

        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            var tokenInUser = _authContext.Users
                .Any(a => a.RefreshToken == refreshToken);
            if (tokenInUser)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }

        //Get the principal (payload) value from the token
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("topsecretkey..........");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principal;
        }

        public async Task<User> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var newToken = resetPasswordDto.EmailToken.Replace(" ", "+");
            var user = await _authContext.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Email == resetPasswordDto.Email);
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }
            var tokenCode = user.ResetPasswordToken;
            DateTime emailTokenExpiry = user.ResetPasswordExpire;
            if (tokenCode != resetPasswordDto.EmailToken || emailTokenExpiry < DateTime.Now)
            {
                throw new Exception("Invalid reset link");
            }

            var pass = CheckPasswrodStrength(resetPasswordDto.NewPassword);
            if (!string.IsNullOrEmpty(pass))
                throw new Exception(pass.ToString());

            user.Password = PasswordHasher.HashPassword(resetPasswordDto.NewPassword);
            _authContext.Entry(user).State = EntityState.Modified;
            await _authContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> SendEmail(string email)
        {
            var user = await _authContext.Users.FirstOrDefaultAsync(a => a.Email == email);
            if (user is null)
            {
                throw new Exception("Email doesn't exist");
            }
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            user.ResetPasswordToken = emailToken;
            user.ResetPasswordExpire = DateTime.Now.AddMinutes(15);
            string from = _config["EmailSettings:From"];
            var emailModel = new EmailModel(email, "Reset Password!!", EmailBody.EmailStringBody(email, emailToken));
            _emailService.SendEmail(emailModel);
            _authContext.Entry(user).State = EntityState.Modified;
            await _authContext.SaveChangesAsync();
            
            return user;
        }
    }
}
