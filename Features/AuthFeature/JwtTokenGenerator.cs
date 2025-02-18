using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Logbook.DataAccess;
using Logbook.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Logbook.Features.AuthFeature;

public class JwtTokenGenerator(IConfiguration configuration, AppDbContext dbContext)
{
    public string GenerateJwtToken(User user)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        ];
        if (user.Role is not null)
        {
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()!));
        }

        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var expires = DateTime.UtcNow.AddMinutes(configuration.GetSection("Jwt:LifetimeMinutes").Get<int>());
        var securityKey = new SymmetricSecurityKey(Constants.DefaultEncoding.GetBytes(configuration["Jwt:Secret"]!));
        var securityToken = new JwtSecurityToken(issuer, audience, claims, expires: expires,
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }

    public string GenerateJwtToken(string refreshToken)
    {
        var tokenEntity = dbContext.RefreshTokens.Include(x => x.User).FirstOrDefault(x => x.Token == refreshToken);

        if (tokenEntity is null)
        {
            throw new RefreshTokenNotFoundException();
        }
        
        var user = tokenEntity.User;
        
        return GenerateJwtToken(user);
    }
}