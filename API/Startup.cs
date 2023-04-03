using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using API.Helpers;
using API.Middleware;
using StackExchange.Redis;
using Infrastructure.Identity;
using API.Extensions;
using Infrastructure.Services;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddControllers();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddDbContext<StoreContext>(options =>
            {
                if (true)
                {
                    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
                }
                else
                {
                    /*
                   * Production Configuration
                   */
                }
            }
            );
            services.AddSingleton<IConnectionMultiplexer>(config => {
                var configuration = ConfigurationOptions.Parse(
                    Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);    
            });
            services.AddDbContext<AppIdentityDbContext>(options => {
                if(true)
                {
                    options.UseSqlite(Configuration.GetConnectionString("IdentityConnection"));
                }
                else
                {
                    /*
                    * Production Configuration
                    */
                }
            });
            // identity service extension method
            services.AddIdentityService();
            services.AddCors(options => options.AddPolicy(
                "CorsPolicy",
                 policy =>
                 {
                     policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(Configuration.GetValue<string>("AngularUrl"));
                 }
                ));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseCustomExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
