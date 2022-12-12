using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TestForOski.Api.Entities;
using TestForOski.Api.Options;

namespace TestForOski.Api.Helpers
{
    public class JwtGenerator : IJwtGenerator
    {
        private AuthOptions _authOptions;

        public JwtGenerator(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions.Value;
        }

        public string GenerateEncodedJwt(Account account)
        {

            var jwt = GenerateJwt(account);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return "Bearer " + encodedJwt;
        }

        private JwtSecurityToken GenerateJwt(Account account)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                        issuer: _authOptions.ISSUER,
                        audience: _authOptions.AUDIENCE,
                        notBefore: now,
                        claims: new List<Claim>
                        {
                            new Claim(ClaimsIdentity.DefaultRoleClaimType,
                                    account.Role),
                            new Claim(ClaimConstants.EmailClaim, account.Email),
                            new Claim(ClaimConstants.IdClaim, account.Id.ToString()),
                            new Claim(ClaimConstants.Name, account.Name)
                        },
                        expires: now.Add(TimeSpan.FromMinutes(_authOptions.LIFETIME)),
                        signingCredentials:
                                new SigningCredentials(
                                        _authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                        );

            return jwt;
        }
    }
}
