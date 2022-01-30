using crm.infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure.Utils
{
    internal class JwtTokenFactory : IJwtTokenFactory
    {
        private readonly IConfiguration _configuration;

        public JwtTokenFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfigurationSection JwtSettings
        {
            get => _configuration.GetSection("JwtSettings");
        }

        public string GenerateToken(User user)
        {

            var claims = new List<Claim>()
            {
                new Claim("permissions", "read:leads"),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Iss, JwtSettings["Issuer"]),
                new Claim(JwtRegisteredClaimNames.Aud, JwtSettings["Audience"]),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
            };

            return SetClaimsToToken(claims, JwtSettings["SecretKey"]);
        }


        public string GenerateIdToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id),
                new Claim("Name", user.UserName),
                new Claim("Role", "Consultant"),
                new Claim("Email", user.Email),
                new Claim(JwtRegisteredClaimNames.Iss, JwtSettings["Issuer"]),
                new Claim(JwtRegisteredClaimNames.Aud, JwtSettings["Audience"]),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
            };

            return SetClaimsToToken(claims, JwtSettings["IDSecretKey"]);
        }


        private string SetClaimsToToken(List<Claim> claims, string secret)
        {
            var jwtPayload = new JwtPayload(claims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtHeader = new JwtHeader(signingCredentials);

            var jwtToken = new JwtSecurityToken(jwtHeader, jwtPayload);

            return new JwtSecurityTokenHandler()
                .WriteToken(jwtToken)
                .ToString();
        }
    }
}
