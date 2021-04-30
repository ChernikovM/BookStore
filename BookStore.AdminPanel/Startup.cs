using BookStore.AdminPanel.Handlers;
using BookStore.AdminPanel.Services;
using BookStore.AdminPanel.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using System.Net.Http;

namespace BookStore.AdminPanel
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AddPageRoute("/Login", "");
                    options.Conventions.AuthorizePage("/UserManagement");
                    options.Conventions.AuthorizePage("/PrintingEditionsManagement");
                });

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<ICookieService, CookieService>();
            services.AddSingleton<IAuthorizationService, AuthorizationService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.Cookie.HttpOnly = true;
                  options.Cookie.SecurePolicy = CurrentEnvironment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
                  options.Cookie.SameSite = SameSiteMode.Lax;
                  options.LoginPath = "/Login";
                  options.LogoutPath = "/Login/Logout";
              });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.None;
                options.Secure = CurrentEnvironment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
            });

            services.AddHttpContextAccessor();
            services.AddTransient<ResponseHandler>();
            services.AddHttpClient("ResponseHandler").AddHttpMessageHandler<ResponseHandler>();

            services.AddScoped<IHttpService, HttpService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/ErrorPage?code={0}");

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
