using AutoMapper;
using BookStore.BusinessLogicLayer.Configurations;
using BookStore.BusinessLogicLayer.Configurations.Interfaces;
using BookStore.BusinessLogicLayer.Mapping;
using BookStore.BusinessLogicLayer.Services;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities;
using BookStore.PresentationLayer.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
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

            services.AddControllers();

            services.AddDbContext<DataContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("Server"))
            );

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
            
            services.AddScoped<IAccountService, AccountService>();

            var loggerConfig = 
                Configuration.GetSection("LoggerConfiguration").Get<LoggerConfiguration>();
            services.AddSingleton<ILoggerConfiguration>(loggerConfig);
            services.AddSingleton<ILoggerService, LoggerService>();

            var mapperConfig = new MapperConfiguration(
                mc => mc.AddProfile<MappingProfile>());
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton<IMapper>(mapper);

            var emailSenderConfig = 
                Configuration.GetSection("EmailSenderConfiguration").Get<EmailSenderConfiguration>();
            services.AddSingleton<IEmailSenderConfiguration>(emailSenderConfig);
            services.AddScoped<IEmailSenderService, EmailSenderService>();

            var jwtConfig =
                Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            services.AddSingleton<IJwtConfiguration>(jwtConfig);
            services.AddScoped<IJwtService, JwtService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
                        ValidAudience = jwtConfig.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true, //TODO: неправильно чекает время жизни токена (пропускает с протухшим токеном)
                    };
                    x.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            
            AuthorizationPolicyBuilder policyBuilder = 
                new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            policyBuilder.RequireAuthenticatedUser();
            services.AddAuthorization(options => options.DefaultPolicy = policyBuilder.Build());
            
            /*
            services.AddAuthorization(options =>
            {
                options.AddPolicy("MyAccessPolicy", policy => policy.RequireAssertion(context =>
                {
                if (context.User.Identity.IsAuthenticated == true)
                    return true;
                else
                    return false;
                }));
            });
            */

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

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
