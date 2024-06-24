
using ModeloApi.Application.DTOs.Login;

namespace ModeloApi.Application.Services.Interfaces;
public interface ILoginService
{
    Task<ResultService<dynamic>> GenerateTokenAsync(LoginDto loginDto);
}
