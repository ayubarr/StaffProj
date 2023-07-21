using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StaffProj.DAL.SqlServer;
using StaffProj.Domain.Models.Abstractions.BaseUsers;
using StaffProj.Domain.Models.Entities;
using StaffProj.Services.Implemintations;
using StaffProj.Services.Interfaces;
using System.Text;

namespace StaffProj.API
{
    public static class Initializer
    {
        public static IServiceCollection InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(UserManager<>));
            return services;
        }
        public static IServiceCollection InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthManager<Employee>, AuthManager<Employee>>();
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            return services;
        }

        public static IServiceCollection InitializeIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<RoleManager<IdentityRole>>();

            services.AddScoped<IAuthManager<Employee>>(provider =>
            {
                var userManager = provider.GetRequiredService<UserManager<Employee>>();
                var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
                return new AuthManager<Employee>(userManager, roleManager, configuration);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                 .AddEntityFrameworkStores<PgDbContext>()
                 .AddDefaultTokenProviders();

            services.AddScoped<IUserStore<Employee>, UserStore<Employee, IdentityRole, AppDbContext, string>>();
            services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();

            return services;
        }
    }
}
