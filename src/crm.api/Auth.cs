using crm.common.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace crm.api
{
    public static class Auth
    {
        public static IServiceCollection AddAuth0(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["Domain"];
                    options.Audience = configuration["Audience"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:leads", policy => policy.Requirements.Add(new HasScopeRequirement("read:leads", configuration["Domain"])));
                options.AddPolicy("read:lead", policy => policy.Requirements.Add(new HasScopeRequirement("read:lead", configuration["Domain"])));
                options.AddPolicy("write:leads", policy => policy.Requirements.Add(new HasScopeRequirement("write:leads", configuration["Domain"])));
                options.AddPolicy("close:lead", policy => policy.Requirements.Add(new HasScopeRequirement("close:lead", configuration["Domain"])));
                
                options.AddPolicy("read:notes", policy => policy.Requirements.Add(new HasScopeRequirement("read:notes", configuration["Domain"])));
                options.AddPolicy("write:note", policy => policy.Requirements.Add(new HasScopeRequirement("write:note", configuration["Domain"])));
                options.AddPolicy("update:note", policy => policy.Requirements.Add(new HasScopeRequirement("update:note", configuration["Domain"])));
                options.AddPolicy("delete:note", policy => policy.Requirements.Add(new HasScopeRequirement("delete:note", configuration["Domain"])));

                options.AddPolicy("read:client", policy => policy.Requirements.Add(new HasScopeRequirement("read:client", configuration["Domain"])));
                options.AddPolicy("update:client", policy => policy.Requirements.Add(new HasScopeRequirement("update:client", configuration["Domain"])));
            });

            return services;
        }
    }
}
