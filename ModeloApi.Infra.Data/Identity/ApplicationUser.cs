
using Microsoft.AspNetCore.Identity;

namespace ModeloApi.Infra.Data.Identity;
public class ApplicationUser : IdentityUser
{
    public string? Refreshtoken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
