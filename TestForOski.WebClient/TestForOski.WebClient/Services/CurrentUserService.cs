using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using TestForOski.WebClient.Constants;
using TestForOski.WebClient.Services.Interfaces;

namespace TestForOski.WebClient.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private long _accountId;
        private string _email;
        private string _name;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long AccountId
        {
            get
            {
                if (_accountId == default)
                {
                    var accountId = GetClaimValue(claimType: ClaimConstants.IdClaim);
                    if (!long.TryParse(accountId, out _accountId))
                    {
                        throw new UnauthorizedAccessException("Not authorized!");
                    }
                }
                return _accountId;
            }
        }

        public string Email
        {
            get
            {
                if (_email is null)
                {
                    _email = GetClaimValue(ClaimConstants.EmailClaim);
                    if (_email is null)
                    {
                        throw new UnauthorizedAccessException("Not authorized!");
                    }
                }
                return _email;
            }
        }

        public string Name
        {
            get
            {
                if (_name is null)
                {
                    _name = GetClaimValue(ClaimConstants.Name);
                    if (_name is null)
                    {
                        throw new UnauthorizedAccessException("Not authorized!");
                    }
                }
                return _name;
            }
        }
        public string Role
        {
            get
            {
                string role = GetClaimValue(ClaimsIdentity.DefaultRoleClaimType);

                if (role.Length <= 0)
                {
                    throw new UnauthorizedAccessException("Not authorized!");
                }
                return role;
            }
        }

        private string GetClaimValue(string claimType)
        {
            return _httpContextAccessor.HttpContext
                .User?
                .Claims?
                .SingleOrDefault(claim => claim.Type == claimType)?
                .Value;
        }
    }
}
