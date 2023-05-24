using API.Extensions;
using API.Helpers;
using API.Middleware;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Data.Seeding;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;

var builder = WebApplication.CreateBuilder(args);


// add services to the container


// Database Related
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<StoreContext>(options =>
{
    if (builder.Environment.EnvironmentName == "Development")
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
    else
    {
        /*
       * Production Configuration
       */
    }
}
);

// Add the identity database
builder.Services.AddDbContext<AppIdentityDbContext>(options => {
    if (builder.Environment.EnvironmentName == "Development")
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection"));
    }
    else
    {
        /*
        * Production Configuration
        */
    }
});
builder.Services.AddSingleton<IConnectionMultiplexer>(config => {
    var configuration = ConfigurationOptions.Parse(
        builder.Configuration.GetConnectionString("Redis"), true);
    return ConnectionMultiplexer.Connect(configuration);
});

// Services
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymantService>();
builder.Services.AddSingleton<IResponseCacheService, ResponseCacheService>();

// third-party libraries
builder.Services.AddAutoMapper(typeof(MappingProfiles));

// identity service extension method
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddCors(options => options.AddPolicy(
    "CorsPolicy",
     policy =>
     {
         policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration.GetValue<string>("AngularUrl"));
     }
    ));
// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});
// Endpoint resolver
builder.Services.AddControllers();

var app = builder.Build();

// Auto Migrations / Seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        // migrating
        var storeContext = services.GetRequiredService<StoreContext>();
        await storeContext.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Migration Failed.");
    }
    try
    {
        // seeding products-related data
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();
        await SeedStoreContext.SeedAsync(unitOfWork);
        
        // seeding users
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
    }
    catch(Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Seeding Failed.");

    }
}
// Configure the HTTP request pipeline


if (app.Environment.EnvironmentName == "Development")
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.UseCustomExceptionPage();

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();