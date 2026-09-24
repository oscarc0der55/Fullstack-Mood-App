using MoodAppBE.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MoodAppBE.Models;
using MoodAppBE.Repository.IRepository;
using MoodAppBE.Repository;
using MoodAppBE.Service.IService;
using MoodAppBE.Service;
using Scalar.AspNetCore;
using MoodAppBE.MiddleWare;

namespace MoodAppBE
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<MoodAppDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentityApiEndpoints<User>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                }).AddRoles<IdentityRole<int>>().AddEntityFrameworkStores<MoodAppDBContext>();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.ConfigureApplicationCookie(option =>
                {
                    option.Cookie.SameSite = SameSiteMode.None;
                    option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                });
            }
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddScoped<IMoodRepository, MoodRepository>();
            builder.Services.AddScoped<IMoodService, MoodService>();
            builder.Services.AddScoped<IWellnessRepository, WellnessRepository>();
            builder.Services.AddScoped<IWellnessService, WellnessService>();
            builder.Services.AddScoped<IUserMoodRepository, UserMoodRepository>();
            builder.Services.AddScoped<IUserMoodService, UserMoodService>();
            builder.Services.AddScoped<IUserWellnessRepository, UserWellnessRepository>();

            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Frontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            var app = builder.Build();

            app.UseCors("Frontend");

            try
            {
                await app.SeedData();
            }
            catch (Exception ex)
            {
                var logger = app.Services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during data seeding. The application will continue, but some data may be incomplete.");
            }

            app.UseMiddleware<GlobalExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<SimpleMiddleware>();
            var api = app.MapGroup("/api");
            app.MapIdentityApi<User>();


            app.MapControllers();

            app.Run();
        }
    }
}
