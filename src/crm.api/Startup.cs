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

namespace crm.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
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
                    options.Authority = Configuration["Domain"];
                    options.Audience = Configuration["Audience"];
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:leads", policy => policy.Requirements.Add(new HasScopeRequirement("read:leads", Configuration["Domain"])));
                options.AddPolicy("read:leaddetails", policy => policy.Requirements.Add(new HasScopeRequirement("read:leaddetails", Configuration["Domain"])));
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
