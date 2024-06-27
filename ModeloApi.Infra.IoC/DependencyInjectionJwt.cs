using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ModeloApi.Infra.IoC;
public static class DependencyInjectionJwt
{
    public static IServiceCollection AddInfraestructureJwt(this IServiceCollection services, IConfiguration configuration)
    {

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer("Bearer", opt =>
        {
            opt.SaveToken = true;
            opt.RequireHttpsMetadata = false;
            opt.TokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = secretKey,
                //Deixa a tolerancia de tempo de expiração como Zero - normalmente é 5 min
                ClockSkew = TimeSpan.Zero,

            };


        });
        return services;
    }
}
