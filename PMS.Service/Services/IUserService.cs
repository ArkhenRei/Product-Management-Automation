using PMS.API.Models;
using PMS.API.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Service.Services
{
    public interface IUserService
    {
        Task<TokenApiDto> Authenticate(User userObj);
        Task<User> RegisterUser(User userObj);
        Task<List<User>> GetAllUsers();
        Task<TokenApiDto> Refresh(TokenApiDto tokenApiDto);
        Task<User> SendEmail(string email);
        Task<User> ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}
