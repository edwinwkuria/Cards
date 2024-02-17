using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cards.API.ConfigModels;
using Cards.Infrastructure.Entities;
using Cards.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Cards.Services;

public class JwtService : IJwtHelper
{
    private readonly JwtConfig _config;

    public JwtService(IOptions<JwtConfig> config)
    {
        _config = config.Value;
    }

    public string GenerateUserToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_config.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Role", user.Role.ToString())
                }
            ),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = _config.Issuer,
            Audience = _config.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        return jwtToken;
    }
}