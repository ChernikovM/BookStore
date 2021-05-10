using BookStore.AdminPanel.Services;
using BookStore.AdminPanel.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

[assembly: HostingStartup(typeof(BookStore.PresentationLayer.Areas.Admin.AdminStartup))]
namespace BookStore.PresentationLayer.Areas.Admin
{
    public class AdminStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddScoped<IAuthorizationService, AuthorizationService>();
                services.AddTransient<IStartupFilter, MyStartupFilter>();

                //services.Configure<CookiePolicyOptions>(options =>
                //{
                //    options.MinimumSameSitePolicy = SameSiteMode.Strict;
                //    options.HttpOnly = HttpOnlyPolicy.None;
                //});

                //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                //    .AddCookie(options =>
                //    {
                //        options.Cookie.HttpOnly = true;
                //        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                //        options.Cookie.SameSite = SameSiteMode.Lax;
                //        options.LoginPath = "/Login";
                //        options.LogoutPath = "/Login/Logout";
                //    });
            });
        }
    }

    public class MyStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseMiddleware<MyMiddleware>();
                //app.UseMiddleware<MyMiddleware>();
                next(app);
            };
        }
    }

    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.User.Identity.IsAuthenticated == true)
            {

                //throw new Exception("auth = true");
            }

            await _next(httpContext);

            if (httpContext.User.Identity.IsAuthenticated == true)
            {
                //throw new Exception("auth = true");
            }

        }
    }
}
