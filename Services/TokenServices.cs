using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Services
{
    public class TokenServices
    {
        private readonly string? _key;

        IConfiguration configuration;
        public TokenServices(IConfiguration config)
        {
            _key = config.GetValue<string>("Key");
            configuration = config;
        }

        public string GenerateJwtToken(string username)
        {
            ;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (_key is not null)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: Convert.ToDateTime(configuration.GetValue<string>("expiration")),  
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return string.Empty;
        }
    }
}