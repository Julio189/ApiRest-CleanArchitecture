
using AutoMapper;
using ModeloApi.Application.DTOs.User;
using ModeloApi.Application.DTOs.Validation.User;
using ModeloApi.Application.Services.Interfaces;
using ModeloApi.Domain.Entities;
using ModeloApi.Domain.Interfaces;

namespace ModeloApi.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<ReadUserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return _mapper.Map<ICollection<ReadUserDto>>(users);
    }

    public async Task<ResultService<ReadUserDto>> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
            return ResultService.Fail<ReadUserDto>($"User id {id} not found!");

        return ResultService.Ok(_mapper.Map<ReadUserDto>(user));
    }

    public async Task<ResultService<ReadUserDto>> CreateUserAsync(CreateUserDto userDto)
    {
        if (userDto == null)
            return ResultService.Fail<ReadUserDto>("Object not found!");

        var validate = new CreateUserDtoValidation().Validate(userDto);

        if (!validate.IsValid)
            return ResultService.RequestError<ReadUserDto>("Fields validate error!", validate);

        var isNameAlreadyExists = await _userRepository.IsNameAlreadyExists(userDto.Name);

        if (isNameAlreadyExists)
            return ResultService.Fail<ReadUserDto>("Name already exists!");

        var password = BCrypt.Net.BCrypt.HashString(userDto.Password);

        var userEntity = _mapper.Map<User>(userDto);

        userEntity.UpdatePassword(password);

        await _userRepository.CreateUserAsync(userEntity);

        return ResultService.Ok(_mapper.Map<ReadUserDto>(userEntity));

    }
    public async Task<ResultService> UpdateUserAsync(UpdateUserDto userDto)
    {
        if (userDto == null)
            return ResultService.Fail("Object not found!");

        var validate = new UpdateUserDtoValidation().Validate(userDto);

        if (!validate.IsValid)
            return ResultService.RequestError("Fields validate error!", validate);

        var userEntity = await _userRepository.GetUserByIdAsync(userDto.Id);

        if (userEntity == null)
            return ResultService.Fail($"User id {userDto.Id} not found!");

        if(userEntity.Name != userDto.Name)
        {
            var isNameAlreadyExists = await _userRepository.IsNameAlreadyExists(userDto.Name);

            if (isNameAlreadyExists)
                return ResultService.Fail("Name already exists!");
        }

        userEntity = _mapper.Map(userDto, userEntity);

        await _userRepository.UpdateUserAsync(userEntity);

        return ResultService.Ok("User updated!");   
    }
    public async Task<ResultService> UpdatePasswordAsync(UpdateUserPasswordDto userDto)
    {
        if (userDto == null)
            return ResultService.Fail("Object not found!");

        var validate = new UpdateUserPasswordDtoValidation().Validate(userDto);

        if (!validate.IsValid)
            return ResultService.RequestError("Fields validate error!", validate);

        if (userDto.Password != userDto.ConfirmPassword)
            return ResultService.Fail("Password and confirm password dont match!");

        var userEntity = await _userRepository.GetUserByIdAsync(userDto.Id);

        if (userEntity == null)
            return ResultService.Fail($"User id {userDto.Id} not found!");

        var password = BCrypt.Net.BCrypt.HashString(userDto.Password);

        userEntity.UpdatePassword(password);

        await _userRepository.UpdateUserAsync(userEntity);

        return ResultService.Ok("Password updated!");
    }

    public async Task<ResultService> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
            return ResultService.Fail($"User id {id} not found!");

        return ResultService.Ok("User deleted!");
    }
}
