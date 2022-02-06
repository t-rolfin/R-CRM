using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using crm.infrastructure;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using crm.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using crm.infrastructure.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using crm.infrastructure.Logging;
using Serilog;

namespace crm.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfigurationSection JwtSettings
        {
            get => Configuration.GetSection("JwtSettings");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>();

            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddSignInManager();

            IoC.Authentification(services, Configuration);
            IoC.Infrastructure(services, Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("UI", x => {
                    x.AllowAnyMethod();
                    x.AllowAnyOrigin();
                    x.AllowAnyHeader();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = JwtSettings["Issuer"],
                        
                        ValidateAudience = true,
                        ValidAudience = JwtSettings["Audience"],

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings["SecretKey"])),
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:leads", policy => policy.RequireClaim("permissions", "read:leads"));
                options.AddPolicy("read:leaddetails", policy => policy.RequireClaim("permissions", "read:leaddetails"));
                options.AddPolicy("write:leads", policy => policy.RequireClaim("permissions", "write:leads"));
            });

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()))
                .AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "crm.api", Version = "v1" });
                c.EnableAnnotations();
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "crm.api v1"));
            }
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseCors("UI");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
