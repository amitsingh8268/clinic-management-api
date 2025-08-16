using ClinicManagement.Infrastructure.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        // JWT bearer (for Authorization attribute)var jwt = builder.Configuration.GetSection("Jwt");
        var jwt = builder.Configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwt["Key"]);

        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwt["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwt["Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30)
            };
        });

        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("FrontEndPolicy", b =>
            {
                b.WithOrigins("http://localhost:4200").AllowCredentials().AllowAnyHeader().AllowAnyMethod();
            });
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("FrontEndPolicy");
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}