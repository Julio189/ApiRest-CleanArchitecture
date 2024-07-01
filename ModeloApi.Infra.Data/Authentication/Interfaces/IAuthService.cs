
using Microsoft.AspNetCore.Mvc;
using ModeloApi.Infra.Data.Authentication.AuthDtos;

namespace ModeloApi.Infra.Data.Authentication.Interfaces;
public interface IAuthService
{
    Task<AuthResultService> Register(RegisterDto registerDto);
    Task<AuthResultService> Login(LoginDto loginDto);
    Task<AuthResultService> CreateRole(RoleDto roleDto);
    Task<AuthResultService> AddUserToRole(string email, string roleName);
    Task<AuthResultService> RefreshToken(TokenDto tokenDto);
    Task<AuthResultService> Revoke(string username);
}
