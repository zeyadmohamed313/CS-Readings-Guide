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
using Core.Dtos;
using AutoMapper;
using Core.Repositories;

namespace Services
{
    public class AuthenticationServices:ResponseHandler,IAuthenticationServices
    {
        #region Fields
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion

        #region Constructor
        public AuthenticationServices(IConfiguration configuration,
            UserManager<AppUser> userManager, IMapper mapper,
            SignInManager<AppUser> signInManager
            ,IUserActivityRepository userActivityRepository, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _userActivityRepository = userActivityRepository;
            _roleManager = roleManager;
        }
        #endregion




        #region HandleFunctions
        public async Task<ApiResponse<string>> Login(string email, string password)
        {
            //Check for the email
            var user = await _userManager.FindByEmailAsync(email);
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



        public async Task<ApiResponse<string>> Register(UserDto user, string password)
        {
            if (user == null || string.IsNullOrEmpty(password))
                return BadRequest<string>("Cannot insert Empty Vale=ue");

            // Mapping 
            var rluser = _mapper.Map<AppUser>(user);

            // Trying To Create User
            var result = await _userManager.CreateAsync(rluser, password);
            await _userManager.AddToRoleAsync(rluser,"User");
            // Creation Feild
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                var errorMessage = string.Join(", ", errors);
                return BadRequest<string>(errorMessage);
            }



            await _userActivityRepository.AddUserLists(rluser);

            // Success
            return Success("Registerd Successfully");
        }

        public async Task<ApiResponse<string>> ChangePassword(string email, string oldpassword,string newpassword)
        {
            //Check for the email
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return BadRequest<string>("Email Is Not Found");

            // Mapping 
            var rluser = _mapper.Map<AppUser>(user);
            // Attempt to change the password
            var result = await _userManager.ChangePasswordAsync(rluser, oldpassword, newpassword);

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
