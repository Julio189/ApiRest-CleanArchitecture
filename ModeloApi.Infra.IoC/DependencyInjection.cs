
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModeloApi.Application.Map;
using ModeloApi.Application.Services;
using ModeloApi.Application.Services.Interfaces;
using ModeloApi.Domain.Authentication;
using ModeloApi.Domain.Interfaces;
using ModeloApi.Infra.Data.Authentication;
using ModeloApi.Infra.Data.Authentication.Interfaces;
using ModeloApi.Infra.Data.Context;
using ModeloApi.Infra.Data.Identity;
using ModeloApi.Infra.Data.Repositories;

namespace ModeloApi.Infra.IoC;
public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgressConexao"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IApplicationUserService, ApplicationUserService>();

        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IPersonService, PersonService>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<IPurchaseService, PurchaseService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();

        services.AddAutoMapper(typeof(DomainToDtoMappingProfile));

        return services;
    }
}
