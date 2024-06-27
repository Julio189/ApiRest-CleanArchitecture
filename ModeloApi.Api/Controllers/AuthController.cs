using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModeloApi.Application.DTOs.AuthenticationDtos;
using ModeloApi.Application.DTOs.Validation.AuthenticationDtos;
using ModeloApi.Application.Services;
using ModeloApi.Infra.Data.Authentication.Interfaces;
using ModeloApi.Infra.Data.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ModeloApi.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] IdentityLoginDto loginDto)
    {
        if (loginDto == null)
            return BadRequest(ResultService.Fail("Object not found!"));

        var validate = new IdentityLoginDtoValidation().Validate(loginDto);

        if (!validate.IsValid)
            return BadRequest(ResultService.RequestError("Fields validate error!", validate));

        var user = await _userManager.FindByNameAsync(loginDto.UserName);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return Unauthorized();

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = _tokenService.GenerateAcessToken(authClaims, _configuration);

        var refreshToken = _tokenService.GenerateRefreshToken();

        _ = int.TryParse(_configuration["JWT:RefreshToken"], out int refreshTokenValidity);

        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidity);

        user.Refreshtoken = refreshToken;

        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            Acess_token = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken,
            Expiration = token.ValidTo,
        });
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register([FromBody] IdentityRegisterDto registerDto)
    {
        if (registerDto == null)
            return BadRequest(ResultService.Fail("Object not found!"));

        var userExists = await _userManager.FindByNameAsync(registerDto.UserName);

        if (userExists is not null)
            return BadRequest(ResultService.Fail("User already exists!"));

        var validate = new IdentityRegisterDtoValidation().Validate(registerDto);

        if (!validate.IsValid)
            return BadRequest(ResultService.RequestError("Fields validate error!", validate));

        ApplicationUser user = new()
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, ResultService.Fail("User creation failed!"));

        return Ok(ResultService.Ok("User created sucessfuly!"));
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<ActionResult> RefreshToken(TokenDto tokenDto)
    {
        if (tokenDto == null)
            return BadRequest(ResultService.Fail("Invalid client request!"));

        string acessToken = tokenDto.AcessToken ?? throw new ArgumentNullException(nameof(tokenDto));

        string refreshToken = tokenDto.RefreshToken ?? throw new ArgumentNullException(nameof(tokenDto));

        var principal = _tokenService.GetPrincipalFromExpiredToken(acessToken, _configuration);

        if (principal == null)
            return BadRequest(ResultService.Fail("Invalid acess token/refresh token!"));

        string username = principal.Identity.Name;

        var user = await _userManager.FindByNameAsync(username);

        if (user == null || user.Refreshtoken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest(ResultService.Fail("Invalid acess token/refresh token!"));

        var newAcessToken = _tokenService.GenerateAcessToken(principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.Refreshtoken = newRefreshToken;

        user.RefreshTokenExpiryTime = DateTime.Now;

        return new ObjectResult(new
        {
            acessToken = new JwtSecurityTokenHandler().WriteToken(newAcessToken),
            refreshToken = newRefreshToken,
        });
    }

    [Authorize]
    [HttpPost]
    [Route("revoke/{username}")]
    public async Task<ActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
            return BadRequest(ResultService.Fail("Invalid username"));

        user.Refreshtoken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
}
