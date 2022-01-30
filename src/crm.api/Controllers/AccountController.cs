using crm.api.AccountModels;
using crm.infrastructure.Identity;
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

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
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

            var result = await _identityService.RegisterUserAsync(user, model.Roles, model.Password);

            return new JsonResult(result);
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LogInModel model)
        {
            var response = await _identityService.LogInAsync(model.UserName, model.Password);
            return Ok(new LoginResonse(Constants.TokenType, response.token, response.idToken , null));
        }

        public record LoginResonse(
            string auth_token_type,
            string auth_token,
            string id_token,
            string refresh_token);
    }
}
