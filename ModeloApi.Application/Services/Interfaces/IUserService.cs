
using ModeloApi.Application.DTOs.User;

namespace ModeloApi.Application.Services.Interfaces;
public interface IUserService
{
    Task<ICollection<ReadUserDto>> GetAllUsersAsync();
    Task<ResultService<ReadUserDto>> GetUserByIdAsync(int id);
    Task<ResultService<ReadUserDto>> CreateUserAsync(CreateUserDto userDto);
    Task<ResultService> UpdateUserAsync(UpdateUserDto userDto);
    Task<ResultService> UpdatePasswordAsync(UpdateUserPasswordDto userDto);
    Task<ResultService> DeleteUserAsync(int id);
}
