using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestForOski.Api.Helpers;
using TestForOski.Api.Models.DTO;
using TestForOski.Api.Services.Interfaces;

namespace TestForOski.Api.Controllers
{
    [Route("account")]
    public class AccountController : ControllerBase
    {
        #region Private readonly fields
        private readonly IAccountService _accountService;
        private readonly IJwtGenerator _jWTGenerator;
        #endregion

        public AccountController(IAccountService accountService, IJwtGenerator jwtGenerator)
        {
            _accountService = accountService;
            _jWTGenerator = jwtGenerator;
        }

        [Route("auth")]
        [HttpPost]
        public async Task<ActionResult> SignIn([FromBody] AuthenticationDto authenticationModel)
        {
            var foundAccount = (await _accountService.GetAccountCredentialsAsync(authenticationModel)).Data;

            #region ValidationChecks

            if (foundAccount == null)
            {
                return Unauthorized("Incorrect credentials, please try again.");
            }

            if (foundAccount.Role.Length <= 0)
            {
                return StatusCode(403, foundAccount.Email + " is registered and waiting assign.");
            }

            #endregion

            var userJwtToken = _jWTGenerator.GenerateEncodedJwt(foundAccount);

            var response = new
            {
                Id = foundAccount.Id,
                Name = foundAccount.Name,
                Role = foundAccount.Role
            };

            GetHeaders(userJwtToken);

            return Ok(response);
        }

        private void GetHeaders(string token)
        {
            Response.Headers.Add("Authorization", token);

            Response.Headers.Add("Access-Control-Expose-Headers", "x-token, Authorization");
        }
    }
}
