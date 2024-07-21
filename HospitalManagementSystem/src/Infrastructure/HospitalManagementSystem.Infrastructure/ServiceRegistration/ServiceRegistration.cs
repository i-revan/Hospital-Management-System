using U = HospitalManagementSystem.Infrastructure.Implementations.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using HospitalManagementSystem.Application.Abstraction.Services.Stripe;
using HospitalManagementSystem.Infrastructure.Implementations.Services;
using HospitalManagementSystem.Application.Common.Email;
using HospitalManagementSystem.Application.Common.Stripe;
using HospitalManagementSystem.Persistence.Implementations.Services;

namespace HospitalManagementSystem.Infrastructure.ServiceRegistration;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenHandler, U.TokenHandler>();
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidAudience = configuration["Jwt:Audience"],
                ValidIssuer = configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"])),
                LifetimeValidator = (_, expired, key, _) => key != null ? expired > DateTime.UtcNow : false
            };
        });
        services.AddAuthorization();
        services.AddMemoryCache();

        services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
        services.AddScoped<IStripeService, StripeService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddSingleton<ICacheService, MemoryCacheService>();

        services.Configure<EmailSettings>(configuration.GetSection("Email"));
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}
