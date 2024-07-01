
namespace ModeloApi.Infra.Data.Authentication.Interfaces;
public interface IApplicationUserService
{
    Task<AuthResultService> GetAllApplicationUsersAsync();
}
