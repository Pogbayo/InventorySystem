

using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InventorySystem.Infrastructure.Jwt_generator
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }


        public string GenerateAccessToken(ApplicationUser user, IList<string> roles)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var key = symmetricSecurityKey;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes).UtcDateTime,
                claims: claims,
                signingCredentials: creds);
            Console.WriteLine($"ExpiryInMinutes that I set by myself: {_jwtSettings.ExpiryInMinutes}");


            Console.WriteLine($"Current Time: {DateTime.UtcNow}");
            Console.WriteLine($"Token Expiration Time (DateTime): {DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes)}");
            Console.WriteLine($"Token Expiration Time (Unix Timestamp): {token.Payload.Exp}");



            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }

}
