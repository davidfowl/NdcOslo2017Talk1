using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyRazorPagesApp
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
            services.AddCookieAuthentication();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            services.AddTwitterAuthentication(options =>
            {
                options.ConsumerKey = Configuration["twitter:consumerKey"];
                options.ConsumerSecret = Configuration["twitter:consumerSecret"];
            });

            services.AddGoogleAuthentication(options =>
            {
                options.ClientId = Configuration["google:clientID"];
                options.ClientSecret = Configuration["google:clientSecret"];
            });

            services.AddMvc();
            services.AddSingleton<IHostedService, RequestCounter>();
            services.AddSingleton<RequestCount>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RequestCount count)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.Use((context, next) =>
            {
                count.AddOne();
                return next();
            });

            app.UseRouter(routes =>
            {
                routes.MapPost("logout", async context =>
                {
                    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    context.Response.Redirect("/");
                });
            });

            app.UseMvc();
        }
    }

    public class RequestCount
    {
        private int _requests;
        public int NumberOfRequests => _requests;

        public void Reset() 
        {
            Interlocked.Exchange(ref _requests, 0);
        }

        public void AddOne() 
        {
            Interlocked.Add(ref _requests, 1);
        }
    }
}
