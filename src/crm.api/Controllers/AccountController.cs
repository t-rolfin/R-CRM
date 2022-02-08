using crm.api.AccountModels;
using crm.common.DTOs;
using crm.infrastructure.Identity;
using crm.infrastructure.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolfin.Result;
using System.Threading.Tasks;

namespace crm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILoggerManager _logger;

        public AccountController(IIdentityService identityService, ILoggerManager logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<JsonResult> Register(RegisterModel model)
        {
            var user = new User()
            {
                UserName = model.FirstName + model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _identityService.RegisterUserAsync(user, model.Roles, model.GeneratePassword, model.Password);

            return new JsonResult(result);
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LogInModel model)
        {
            _logger.LogError("Method LogIn was called!");
            var response = await _identityService.LogInAsync(model.UserName, model.Password);
            return Ok(new LoginResonse(Constants.TokenType, response.token, response.idToken , null));
        }

        [HttpGet("getUserDetails/{id}")]
        public async Task<IActionResult> GetUserDetails(string id)
        {
            var userDetails = await _identityService.GetUserDetailsAsync(id);
            return userDetails is default(UpdateUserModel) ? BadRequest() : Ok(userDetails);
        }

        public record LoginResonse(
            string auth_token_type,
            string auth_token,
            string id_token,
            string refresh_token);
    }
}
