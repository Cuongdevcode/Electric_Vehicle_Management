using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Application.Interfaces;

namespace User.Infrastructure.Authentications
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User.Domain.Entities.User user)
        {
            var jwt = _configuration.GetRequiredSection("JwtSettings");
            var secret = jwt.GetValue<string>("SecretKey");
            var issuer = jwt.GetValue<string>("Issuer");
            var audience = jwt.GetValue<string>("Audience");

            if (string.IsNullOrWhiteSpace(secret))
                throw new InvalidOperationException("Missing JwtSettings:SecretKey");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role?.Name ?? string.Empty),
                    new Claim(ClaimTypes.Name, user.Fullname ?? string.Empty),
                };

            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Support ExpiryInMinutes or ExpireDays; default 60 minutes
            DateTime expiresAt;
            var minutes = jwt.GetValue<int?>("ExpiryInMinutes");
            var days = jwt.GetValue<int?>("ExpireDays");
            if (minutes.HasValue && minutes.Value > 0)
                expiresAt = DateTime.UtcNow.AddMinutes(minutes.Value);
            else if (days.HasValue && days.Value > 0)
                expiresAt = DateTime.UtcNow.AddDays(days.Value);
            else
                expiresAt = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
