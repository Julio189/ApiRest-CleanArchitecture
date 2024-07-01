
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ModeloApi.Infra.Data.Authentication.AuthDtos;
using ModeloApi.Infra.Data.Authentication.AuthDtos.Validation;
using ModeloApi.Infra.Data.Authentication.Interfaces;
using ModeloApi.Infra.Data.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ModeloApi.Infra.Data.Authentication;
public class AuthService : IAuthService
{

    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<AuthResultService> Register(RegisterDto registerDto)
    {
        if (registerDto == null)
            return AuthResultService.Fail("Object not found!");

        var userExists = await _userManager.FindByNameAsync(registerDto.UserName);

        if (userExists is not null)
            return AuthResultService.Fail("User already exists!");

        var validate = new RegisterDtoValidation().Validate(registerDto);

        if (!validate.IsValid)
            return AuthResultService.RequestError("Fields validate error!", validate);

        ApplicationUser user = new()
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
            return AuthResultService.Fail("User creation failed!");

        return AuthResultService.Ok("User created sucessfuly!");
    }

    public async Task<AuthResultService> Login(LoginDto loginDto)
    {
        if (loginDto == null)
            return AuthResultService.Fail("Object not found!");

        var validate = new LoginDtoValidation().Validate(loginDto);

        if (!validate.IsValid)
            return AuthResultService.RequestError("Fields validate error!", validate);

        var user = await _userManager.FindByNameAsync(loginDto.UserName);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return AuthResultService.Fail("User or password is not correct!");

        var token = _tokenService.GenerateAccessToken(user);

        var refreshToken = _tokenService.GenerateRefreshToken();

        _ = int.TryParse(_configuration["JWT:RefreshToken"], out int refreshTokenValidity);

        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidity);

        user.Refreshtoken = refreshToken;

        await _userManager.UpdateAsync(user);

        var tokenResponse = new
        {
            Access_token = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken,
            Expiration = token.ValidTo,
        };
        
        return AuthResultService.SuccessWithData(tokenResponse);
    }

    public async Task<AuthResultService> CreateRole(RoleDto roleDto)
    {
        if (roleDto == null)
            return AuthResultService.Fail("Object not found!");

        var validate = new RoleDtoValidation().Validate(roleDto);

        if (!validate.IsValid)
            return AuthResultService.RequestError("Fields validate error!", validate);

        var roleExists = await _roleManager.RoleExistsAsync(roleDto.Name);

        if (roleExists)
            return AuthResultService.Fail("Role already exists!");

        var role = await _roleManager.CreateAsync(new IdentityRole(roleDto.Name));

        if (!role.Succeeded)
            return AuthResultService.Fail("Erro adding new role");

        return AuthResultService.Ok($"Role {roleDto.Name} added sucessfully!");
    }

    public async Task<AuthResultService> AddUserToRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return AuthResultService.Fail($"User email {email} not found!");

        var role = await _roleManager.FindByNameAsync(roleName);

        if (role == null)
            return AuthResultService.Fail($"Role {roleName} not found!");

        var result = await _userManager.AddToRoleAsync(user, role.Name!);

        if (!result.Succeeded)
            return AuthResultService.Fail($"Unable to add user {user.Email} to role {role.Name}!");

        return AuthResultService.Ok("User add to role sucessufully!");
    }

    public async Task<AuthResultService> RefreshToken(TokenDto tokenDto)
    {
        if (tokenDto == null)
            return AuthResultService.Fail("Invalid client request!");

        string accessToken = tokenDto.AcessToken ?? throw new ArgumentNullException(nameof(tokenDto));

        string refreshToken = tokenDto.RefreshToken ?? throw new ArgumentNullException(nameof(tokenDto));

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

        if (principal == null)
            return AuthResultService.Fail("Invalid access token/refresh token!");

        string username = principal.Identity.Name;

        var user = await _userManager.FindByNameAsync(username);

        if (user == null || user.Refreshtoken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return AuthResultService.Fail("Invalid access token/refresh token!");

        var newAccessToken = _tokenService.GenerateAccessToken(user);

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.Refreshtoken = newRefreshToken;

        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:RefreshToken"]));

        await _userManager.UpdateAsync(user);

        var tokenResponse = new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken,
        };

        return AuthResultService.SuccessWithData(tokenResponse);
    }

    public async Task<AuthResultService> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
            return AuthResultService.Fail("Invalid username");

        user.Refreshtoken = null;

        await _userManager.UpdateAsync(user);

        return AuthResultService.Ok("Sucess!");
    }
}
