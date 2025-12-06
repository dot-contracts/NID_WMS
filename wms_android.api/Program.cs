using wms_android.shared.Data;
using wms_android.shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using wms_android.shared.Models.Auth;
using wms_android.shared.Services;
using Microsoft.Extensions.Options;
using wms_android.api.Services;
using wms_android.api.Interfaces;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuration is automatically loaded from appsettings.json and appsettings.{Environment}.json
        Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");

        // Add services to container
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register services
        builder.Services.AddScoped<IParcelService, ApiParcelService>();

        // Configure database using connection string from appsettings
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Configure Npgsql to use timestamps with time zone by default
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Log the connection attempt (without sensitive info)
        Console.WriteLine($"Using connection string from configuration");

        // Add Health Checks with the explicit connection string
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>();

        // Configure JWT
        var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        builder.Services.AddSingleton(Options.Create(jwtConfig));

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.Audience,
                    ClockSkew = TimeSpan.Zero
                };
            });

        // Configure CORS
        var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new[] { "*" };
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowWebApp",
                policy =>
                {
                    policy.WithOrigins(corsOrigins)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        // Register services
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<IUserService, wms_android.shared.Services.UserService>();
        builder.Services.AddScoped<IVehicleService, VehicleService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ISmsService, wms_android.shared.Services.SmsService>();
        builder.Services.AddScoped<wms_android.api.Interfaces.ITokenService, wms_android.api.Services.TokenService>();

        var app = builder.Build();

        // Apply database migrations on startup
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                dbContext.Database.Migrate();
                logger.LogInformation("Database migration completed successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database");
                throw;
            }
        }

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Middleware pipeline
        app.UseHttpsRedirection();
        app.UseCors("AllowWebApp");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        // Map health checks endpoint last
        app.MapHealthChecks("/health");

        // Add a diagnostic endpoint to help troubleshoot route issues
        app.MapFallback(async context => {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(new { 
                error = "Route not found",
                path = context.Request.Path.Value,
                method = context.Request.Method,
                availableRoutes = "Use /swagger to view available API endpoints"
            });
        });

        app.Run();
    }
}
// var builder = WebApplication.CreateBuilder(args);

// // Force Production environment and its configuration
// builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
//     .AddJsonFile("appsettings.Production.json", optional: false, reloadOnChange: true)
//     .AddEnvironmentVariables();

// // Log the current configuration
// Console.WriteLine($"Current environment: {builder.Environment.EnvironmentName}");
// Console.WriteLine($"Using connection string: {builder.Configuration.GetConnectionString("DefaultConnection")}");

// // Add CORS policy
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowMobileApp",
//         builder => builder
//             .AllowAnyOrigin()
//             .AllowAnyMethod()
//             .AllowAnyHeader());
// });

// // Configure database
// builder.Services.AddDbContext<AppDbContext>(options =>
// {
//     var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//     Console.WriteLine($"Attempting to connect with: {connectionString}");
    
//     options.UseNpgsql(connectionString, npgsqlOptions =>
//     {
//         npgsqlOptions.EnableRetryOnFailure(
//             maxRetryCount: 3,
//             maxRetryDelay: TimeSpan.FromSeconds(10),
//             errorCodesToAdd: new[] { "57P01", "57P02", "57P03" });
//         npgsqlOptions.CommandTimeout(30);
//         npgsqlOptions.MigrationsAssembly("wms_android.api");
//     });
    
//     options.EnableDetailedErrors();
//     options.EnableSensitiveDataLogging();
// });

// // Add services
// builder.Services.AddScoped<IParcelService, ParcelService>();
// builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IVehicleService, VehicleService>();

// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// app.UseCors("AllowMobileApp");

// // Configure the HTTP request pipeline
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();
// app.UseAuthorization();
// app.MapControllers();

// app.Run();

