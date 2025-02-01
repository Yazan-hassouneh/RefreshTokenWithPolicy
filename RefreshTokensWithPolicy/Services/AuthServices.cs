using RefreshTokensWithPolicy.CustomePolicies;
using RefreshTokensWithPolicy.Data;
using RefreshTokensWithPolicy.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RefreshTokensWithPolicy.Repository;

namespace RefreshTokensWithPolicy.Services
{
	public static class AuthServices
	{
		internal static IServiceCollection AddIdentityServices(this IServiceCollection services)
		{
			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddSignInManager()
				.AddRoles<IdentityRole>(); 
			
			return services;
		}		
		internal static IServiceCollection AddJWTServices(this IServiceCollection services, WebApplicationBuilder builder)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(jwtOptions =>
			{
				jwtOptions.RequireHttpsMetadata = false;
				jwtOptions.SaveToken = false;
				jwtOptions.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["JWT:Issuer"],
					ValidAudience = builder.Configuration["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
					ClockSkew = TimeSpan.Zero,
				};
			});
			
			return services;
		}
		internal static IServiceCollection AddPoliciesServices(this IServiceCollection services)
		{
			services.AddAuthorizationBuilder()
				.AddPolicy("AdminManagerUserPolicy", options =>
				{
					options.RequireAuthenticatedUser();
					options.RequireRole("admin", "manager", "user");
				})
				.AddPolicy("AdminManagerPolicy", options =>
				{
					options.RequireAuthenticatedUser();
					options.RequireRole("admin", "manager");
				})
				.AddPolicy("AdminUserPolicy", options =>
				{
					options.RequireAuthenticatedUser();
					options.RequireRole("admin", "user");
					options.Requirements.Add(new MinimumAgeRequirement(18));
				});

			return services;
		}		
		internal static IServiceCollection AddCustomPoliciesServices(this IServiceCollection services)
		{
			services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();

			return services;
		}
		internal static IServiceCollection AddRepository(this IServiceCollection services)
		{
			services.AddScoped<IAuthRepo, AuthRepo>();
			return services;
		}
	}
}
