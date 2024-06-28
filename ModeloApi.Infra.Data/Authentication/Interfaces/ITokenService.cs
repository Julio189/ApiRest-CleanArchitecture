
using Microsoft.Extensions.Configuration;
using ModeloApi.Infra.Data.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ModeloApi.Infra.Data.Authentication.Interfaces;
public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(ApplicationUser user);

    public string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
