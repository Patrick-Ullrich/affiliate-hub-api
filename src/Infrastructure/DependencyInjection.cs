using AffiliateHub.Application.Common.Interfaces;
using AffiliateHub.Infrastructure.Persistence;
using AffiliateHub.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AffiliateHub.Infrastructure.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AffiliateHub.Application.Users.Services.Interfaces;
using AffiliateHub.Application.Users.Services;
using Microsoft.Extensions.Configuration;

namespace AffiliateHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("AffiliateHubDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
        services.AddSingleton<IAuthService>(
            new AuthService(
                configuration.GetValue<string>("Jwt:Secret"),
                configuration.GetValue<int>("Jwt:LifeSpan"),
                configuration.GetValue<string>("Jwt:Issuer"),
                configuration.GetValue<string>("Jwt:Audience")
            )
        );
        services.AddTransient<IDateTime, DateTimeService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
                        ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Secret"))
                    )
                    };
                });

        return services;
    }
}
