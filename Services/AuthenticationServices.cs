using Core.Entites.Identity;
using Core.ResponseSchema;
using Core.Services;
using Core.ResponseSchema;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationServices:ResponseHandler,IAuthenticationServices
    {
        #region Fields
        private readonly IConfiguration _configuration;
        UserManager<AppUser> _userManager;
        #endregion

        #region Constructor
        public AuthenticationServices(IConfiguration configuration,UserManager<AppUser> userManager)
        {
            _configuration = configuration;   
            _userManager = userManager;
        }
        #endregion




        #region HandleFunctions
        public async Task<ApiResponse<string>> Login(string email, string password)
        {
            //Check for the email
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
                return BadRequest<string>("Email Is Not Found");


            // Check Password
            bool CheckPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!CheckPassword)
                return BadRequest<string>("Password Is Wrong");


            // Generating Token 
            var Token = GenerateJWTToken(user);
            return Success(Token);
        }



        public async Task<ApiResponse<string>> Register(AppUser user, string password)
        {
            // Trying To Create User
            var result = await _userManager.CreateAsync(user, password);

            // Creation Feild
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                var errorMessage = string.Join(", ", errors);
                return BadRequest<string>(errorMessage);
            }

            // Success
            return Success("Registerd Successfully");
        }


        public async Task<ApiResponse<string>> ChangePassword(AppUser user, string oldpassword,string newpassword)
        {
            // Attempt to change the password
            var result = await _userManager.ChangePasswordAsync(user, oldpassword, newpassword);

            // Check If The Process Successed
            if (!result.Succeeded)
              return  BadRequest<string>("Change Password Has Falied");

            return Success("Password Changed Successfully");
        }

        #endregion





        #region HelperFunctions
        private string GenerateJWTToken(AppUser user)
        {
            //claims token
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            //    signingCredentials 
            SigningCredentials signing = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256
            );


            JwtSecurityToken MyToken = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],//url for web api << provider
            audience: _configuration["JWT:ValidAudiance"], // url consumer << angular
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: signing
            );
            return new JwtSecurityTokenHandler().WriteToken(MyToken);

        }
        #endregion
    }
}
