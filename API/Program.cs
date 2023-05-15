using API.Extensions;
using API.Helpers;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
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
builder.Services.AddScoped<IJwtService, JwtService>();
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
    var securityScheme = new OpenApiSecurityScheme
    {
        Description = "JWT Auth Bearer Scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference()
        {
            Type = ReferenceType.SecurityScheme,
            Id = "bearer"
        }
    };
    c.AddSecurityDefinition("bearer", securityScheme);
    var securityRequirement = new OpenApiSecurityRequirement()
                {
                    { securityScheme, new[]{"bearer"} }
                };
    c.AddSecurityRequirement(securityRequirement);
});
// Endpoint resolver
builder.Services.AddControllers();

var app = builder.Build();

// Auto Migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Migration Failed.");
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
await app.RunAsync();