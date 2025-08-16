using ClinicManagement.Application.Features;
using ClinicManagement.Application.Features.Auth;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Infrastructure.Persistence;
using ClinicManagement.Infrastructure.Repositories;
using ClinicManagement.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManagement.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register MediatR handlers (if you use MediatR)
            services.AddMediatR(cfg =>
             cfg.RegisterServicesFromAssemblies(
                 typeof(LoginCommandHandler).Assembly,
                 typeof(AddUserCommandHandler).Assembly,
                 typeof(RefreshTokenCommandHandler).Assembly,
                 typeof(LogoutCommandHandler).Assembly
             ));

            // Register FluentValidation validators (if you use FluentValidation)
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // You can register other application services here
            // services.AddScoped<IUserService, UserService>();

            return services;
        }
         public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(config.GetConnectionString("ConnectionString")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IPasswordHasher, BcryptPasswordHasher>(); // simple wrapper for BCrypt
            return services;
        }
    }
}
