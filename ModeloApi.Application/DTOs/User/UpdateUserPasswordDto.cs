
namespace ModeloApi.Application.DTOs.User;
public class UpdateUserPasswordDto
{
    public int Id { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
