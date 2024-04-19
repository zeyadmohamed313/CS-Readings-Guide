using Core.Dtos;
using Core.Entites.Identity;
using Core.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IAuthenticationServices
    {
        public Task<ApiResponse<string>> ChangePassword(string email, string oldpassword, string newpassword);
        public Task<ApiResponse<string>> Register(UserDto user, string password);
        public Task<ApiResponse<string>> Login(string email, string password);

    }
}
