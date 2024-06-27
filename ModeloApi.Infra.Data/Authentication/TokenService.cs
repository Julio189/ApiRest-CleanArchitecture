
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModeloApi.Infra.Data.Authentication.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ModeloApi.Infra.Data.Authentication;
public class TokenService : ITokenService
{
    public JwtSecurityToken GenerateAcessToken(ICollection<Claim> claims, IConfiguration config)
    {
        var key = config.GetSection("JWT").GetValue<string>("SecretKey") ??
            throw new InvalidOperationException("Invalid secret key!");

        var secretKey = Encoding.UTF8.GetBytes(key);

        var credentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(config.GetSection("JWT").GetValue<double>("ExpirationToken")),
            Audience = config.GetSection("JWT").GetValue<string>("Audience"),
            Issuer = config.GetSection("JWT").GetValue<string>("Issuer"),
            SigningCredentials = credentials,
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return token;
    }

    public string GenerateRefreshToken()
    {
        var secureRandomBytes = new byte[128];

        using var radomNumberGenerator = RandomNumberGenerator.Create();

        radomNumberGenerator.GetBytes(secureRandomBytes);

        var refreshToken = Convert.ToBase64String(secureRandomBytes);

        return refreshToken;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration config)
    {
        var securityKey = config["JWT:SecretKey"] ?? throw new InvalidOperationException("Invalid security key!");

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token!");
        }

        return principal;
    }
}
