using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalProjectAPI.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string userString)
        {
            string key = _configuration.GetValue("Jwt:Key", string.Empty)
                ?? throw new InvalidOperationException("JWT key is missing");

            int expiration = _configuration.GetValue("Jwt:ExpiryInMinutes", 15);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userString)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                //expires: DateTime.Now.AddMinutes(expiration),
                //claims: claims,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
