using Core.Dtos;
using Core.Services;
using CS_Readings_Guide.Base;
using CS_Readings_Guide.Router;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CS_Readings_Guide.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        #region Feilds
        private readonly IAuthenticationServices _authenticationServices;

        #endregion
        #region Constructor
        public AuthenticationController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }
        #endregion
        #region HandleActions 
        [HttpPost]
        [Route(RoutingSchema.AuthenticationRouting.Register)]
        [SwaggerOperation(Summary = "التسجيل", OperationId = "Register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            var result = await _authenticationServices.Register(user, user.Password);
            return NewResult(result);
        }

        [HttpPost]
        [Route(RoutingSchema.AuthenticationRouting.Login)]
        [SwaggerOperation(Summary = "تسجيل الدخول", OperationId = "Login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            var result = await _authenticationServices.Login(user.Email, user.Password);
            return NewResult(result);
        }

        [HttpPut]
        [Route(RoutingSchema.AuthenticationRouting.ChangePassword)]
        [SwaggerOperation(Summary = "تغيير الرقم السري", OperationId = "ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto cng)
        {
            var result = await _authenticationServices.ChangePassword(cng.Email, cng.OldPassword,cng.NewPassword);
            return NewResult(result);
        }


        #endregion
    }
}
