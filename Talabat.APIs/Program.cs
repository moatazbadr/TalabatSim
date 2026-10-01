using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;
using Talabat.APIs.Extensions;
using StackExchange.Redis;
using Talabat.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Talabat.Domain.Entities.Identity;
using Talabat.APIs.Middlewares;

namespace Talabat.APIs;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Adding services to the container
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddDbContext<StoreContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
            return ConnectionMultiplexer.Connect(configuration);
        });

        builder.Services.AddDbContext<AppIdentityDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
        });



        builder.Services.AddApplicationServices();



        builder.Services.AddIdentityServices(builder.Configuration);
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
        });
        // Inside app pipeline:


        #endregion

        var app = builder.Build();


        #region Updating the database Automatically


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMiddleware<ExceptionMiddleWare>();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Automatically apply pending migrations on startup
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                // Replace 'TalabatDbContext' with your actual DbContext class name
                var context = services.GetRequiredService<StoreContext>();
               // await context.Database.MigrateAsync();

                var identityContext = services.GetRequiredService<AppIdentityDbContext>();
               // await identityContext.Database.MigrateAsync();

                var userManager = services.GetRequiredService<UserManager<AppUser>>();


                await AppIdentityDbContextSeed.SeedUserAsync(userManager);
                await StoreContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while applying EF Core migrations.");
            }
        }
        #endregion



        #region Configure - Configure the HTTP request pipeline

        app.UseCors("CorsPolicy");
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();
        #endregion

        app.Run();
    }
}
