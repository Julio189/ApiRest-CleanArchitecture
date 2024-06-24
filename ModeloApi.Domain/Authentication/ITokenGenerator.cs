
using ModeloApi.Domain.Entities;

namespace ModeloApi.Domain.Authentication;
public interface ITokenGenerator
{
    dynamic Generator(User user);
}
