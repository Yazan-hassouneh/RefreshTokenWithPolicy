using Microsoft.EntityFrameworkCore;
using RefreshTokensWithPolicy.Data;
using RefreshTokensWithPolicy.Helpers;
using RefreshTokensWithPolicy.Services;

namespace RefreshTokensWithPolicy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
			builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
					builder.Configuration.GetConnectionString("DefaultConnection")
				));
			builder.Services.AddIdentityServices();
			builder.Services.AddJWTServices(builder);
			builder.Services.AddPoliciesServices();
			builder.Services.AddCustomPoliciesServices();
			builder.Services.AddRepository();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            // Important to add 
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
