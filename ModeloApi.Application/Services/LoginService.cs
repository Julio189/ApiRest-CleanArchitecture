
using BCrypt.Net;
using ModeloApi.Application.DTOs.Login;
using ModeloApi.Application.DTOs.Validation.Login;
using ModeloApi.Application.Services.Interfaces;
using ModeloApi.Domain.Authentication;
using ModeloApi.Domain.Interfaces;

namespace ModeloApi.Application.Services;
public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginService(IUserRepository userRepository, ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResultService<dynamic>> GenerateTokenAsync(LoginDto loginDto)
    {
        if (loginDto == null)
            return ResultService.Fail<dynamic>("Object not found!");

        var validate = new LoginDtoValidation().Validate(loginDto);

        if (!validate.IsValid)
            return ResultService.RequestError<dynamic>("Fields validate error!", validate);

        var user = await _userRepository.GetUserByName(loginDto.Name);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            return ResultService.Fail<dynamic>("User or password is not correct!");

        return ResultService.Ok(_tokenGenerator.Generator(user));
    }
}
