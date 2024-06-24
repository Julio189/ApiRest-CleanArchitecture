
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModeloApi.Domain.Authentication;
using ModeloApi.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ModeloApi.Infra.Data.Authentication;
public class TokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _configuration;

    public TokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public dynamic Generator(User user)
    {
        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Name", user.Name),
            new Claim("Group", user.Group),
            new Claim("Status", user.Status.ToString())
        }; 

        var experation = DateTime.UtcNow.AddDays(1);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: experation,
                signingCredentials: credentials
            );

        return new
        {
            acess_token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = experation
        };
    }
}
