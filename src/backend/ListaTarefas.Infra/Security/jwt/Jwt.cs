using Domain.Entities;
using ListaTarefas.Infra.Security.jwt.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankMore.Application.service.jwt
{
    public class Jwt : IJwt
    {
        
        private readonly string _jwtKey;
        private readonly string _audience;
        private readonly string _issuer;
        private readonly int _expiresInMinutes;
        
        public Jwt(IOptions<JwtSettings> options)
        {
            _jwtKey = options.Value.SecretKey;
            _expiresInMinutes = Convert.ToInt32(options.Value.ExpiresInMinutes);
            _audience = options.Value.Audience;
            _issuer = options.Value.Issuer;
        }
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("email", user.Email),
            //    new Claim("status", user.IsActive.ToString())
            };

            return GenerateToken(claims);
        }
        public string GenerateToken(string userId, string email, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Adicionar roles como claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return GenerateToken(claims);
        }
        private string GenerateToken(IEnumerable<Claim> claims)
        {

            var secretKey = Encoding.ASCII.GetBytes(_jwtKey);
            var expiresInMinutes = _expiresInMinutes;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

