using AutoMapper;
using BookStore.BusinessLogicLayer.Configurations;
using BookStore.BusinessLogicLayer.Configurations.Interfaces;
using BookStore.BusinessLogicLayer.Mapping;
using BookStore.BusinessLogicLayer.Providers;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Enums;
using BookStore.DataAccessLayer.Repositories.EFRepositories;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using BookStore.PresentationLayer.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
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
                options => options.UseSqlServer(Configuration.GetConnectionString("Server")));

            /*
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            */

            //TODO: requireUniqueEmail set true
            services.AddIdentity<User, IdentityRole>(x => x.User.RequireUniqueEmail = false)
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPrintingEditionService, PrintingEditionService>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IPrintingEditionRepository, PrintingEditionRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            var loggerConfig = 
                Configuration.GetSection("LoggerConfiguration").Get<LoggerConfiguration>();
            services.AddSingleton<ILoggerConfiguration>(loggerConfig);
            services.AddSingleton<ILoggerProvider, LoggerProvider>();

            var mapperConfig = new MapperConfiguration(
                mc => mc.AddProfile<MappingProfile>());
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton<IMapper>(mapper);

            var emailSenderConfig = 
                Configuration.GetSection("EmailSenderConfiguration").Get<EmailSenderConfiguration>();
            services.AddSingleton<IEmailSenderConfiguration>(emailSenderConfig);
            services.AddScoped<IEmailSenderProvider, EmailSenderProvider>();

            var jwtConfig =
                Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            services.AddSingleton<IJwtConfiguration>(jwtConfig);
            services.AddScoped<IJwtProvider, JwtProvider>();

            var dataCollectionAccessConfig =
                Configuration.GetSection("DataCollectionAccessConfiguration").Get<DataCollectionAccessServiceConfiguration>();
            services.AddSingleton<IDataCollectionAccessProviderConfiguration>(dataCollectionAccessConfig);
            services.AddScoped<IDataCollectionAccessProvider, DataCollectionAccessProvider>();

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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.SecretAccessToken)),
                        ValidAudience = jwtConfig.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
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

            var adminPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .RequireRole(Enums.Roles.Admin.ToString())
                .Build();

            services.AddAuthorization(options => options.DefaultPolicy = policyBuilder.Build());

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", adminPolicy);
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                { 
                    Version = "v1",
                    Title = "BookStore API"
                });
            });

            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy( builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader();
                    });
            });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
