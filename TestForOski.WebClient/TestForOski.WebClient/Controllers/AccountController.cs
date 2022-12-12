using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestForOski.WebClient.Constants;
using TestForOski.WebClient.Models.Account;
using TestForOski.WebClient.Utils.Interfaces;

namespace TestForOski.WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOptions<ApplicationSettings> _config;
        private readonly IApiUtil _apiUtil;
        private readonly IDataProtector _protector;
        private readonly AccountsApiEndpoints _accountsApiEndpoints;

        public AccountController(IOptions<ApplicationSettings> config,
                                 IApiUtil apiUtil,
                                 IDataProtectionProvider provider)
        {
            _apiUtil = apiUtil;
            _config = config;
            _accountsApiEndpoints = _config.Value.Urls.ApiEndpoints.Accounts;

            _protector = provider.CreateProtector(_config.Value.Cookies.SecureKey);
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationDto authDto)
        {
            var token = await _apiUtil.SignInAsync(_accountsApiEndpoints.SignIn, authDto);

            if (token == null)
            {
                ModelState.AddModelError(string.Empty, "Incorrect Email and/or Password");
                return View(authDto);
            }

            token = token.Replace("Bearer ", "");

            if (token == null || !await Authenticate(token))
            {
                return RedirectToAction("Login", "Account");
            }

            SetResponseCookie("accessToken", _protector.Protect(token));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }

        private async Task<bool> Authenticate(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var tokenS = handler.ReadToken(token) as JwtSecurityToken;

            var role = tokenS.Claims.First(claim => claim.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            var accountId = tokenS.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.IdClaim).Value;
            var email = tokenS.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.EmailClaim).Value;
            var name = tokenS.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.Name).Value;

            SetResponseCookie("currentRole", role);
            SetResponseCookie("accountId", accountId);
            SetResponseCookie("email", email);
            SetResponseCookie("name", name);

            var claims = new List<Claim>
            {
                 new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                 new Claim(ClaimConstants.IdClaim, accountId),
                 new Claim(ClaimConstants.EmailClaim, email),
                 new Claim(ClaimConstants.Name, name)
            };

            ClaimsIdentity roleClaim = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(roleClaim));

            return true;
        }

        private void SetResponseCookie(string key, string value)
        {
            Response.Cookies.Append(key, value, new CookieOptions()
            {
                SameSite = SameSiteMode.Lax,
                Path = "/",
                Secure = true
            });
        }
    }
}
