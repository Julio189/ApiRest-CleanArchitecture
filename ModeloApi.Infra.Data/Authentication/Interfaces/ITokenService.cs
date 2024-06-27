
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ModeloApi.Infra.Data.Authentication.Interfaces;
public interface ITokenService
{
    JwtSecurityToken GenerateAcessToken(ICollection<Claim> claims, IConfiguration config);

    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration config);
}
