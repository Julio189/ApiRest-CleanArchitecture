
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModeloApi.Infra.Data.Authentication.AuthDtos;
using ModeloApi.Infra.Data.Authentication.Interfaces;
using ModeloApi.Infra.Data.Identity;

namespace ModeloApi.Infra.Data.Authentication;
public class ApplicationUserService : IApplicationUserService
{

    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AuthResultService> GetAllApplicationUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var usersDto = users.Select(user => new ReadApplicationUserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        }).ToList();

        return AuthResultService.SuccessWithData(usersDto);
    }
}
