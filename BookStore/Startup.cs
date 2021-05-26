using AutoMapper;
using BookStore.BusinessLogicLayer.Configurations;
using BookStore.BusinessLogicLayer.Configurations.Interfaces;
using BookStore.BusinessLogicLayer.Mapping;
using BookStore.BusinessLogicLayer.Policies.Authentication;
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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

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

            //TODO: requireUniqueEmail set true
            services.AddIdentity<User, IdentityRole>(x => x.User.RequireUniqueEmail = false)
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPrintingEditionService, PrintingEditionService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IPrintingEditionRepository, PrintingEditionRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            var loggerConfig = 
                Configuration.GetSection("LoggerConfiguration").Get<LoggerConfiguration>();
            services.AddSingleton<ILoggerConfiguration>(loggerConfig);
            services.AddSingleton<ILoggerProvider, LoggerProvider>();

            var stripeConfig =
                Configuration.GetSection("StripeConfiguration").Get<StripeConfiguration>();
            services.AddSingleton<IStripeConfiguration>(stripeConfig);
            services.AddScoped<IPaymentStripeService, PaymentStripeService>();

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
                });


            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

                var adminCookiePolicyBuilder = new AuthorizationPolicyBuilder(
                        CookieAuthenticationDefaults.AuthenticationScheme);
                adminCookiePolicyBuilder = adminCookiePolicyBuilder
                                                .RequireAuthenticatedUser()
                                                .RequireRole(Enums.Roles.Admin.ToString());
                options.AddPolicy("AdminCookiePolicy", adminCookiePolicyBuilder.Build());
            });

            ////////////////Admin Cookie Policy
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.LoginPath = "/Admin/Account/Login";
                    options.LogoutPath = "/Admin/Account/Logout";
                    options.AccessDeniedPath = "/Admin/Account/Login";
                    options.EventsType = typeof(CookieAuthenticationPolicyHandler);
                    
                });

            services.AddScoped<CookieAuthenticationPolicyHandler>();
            services.AddScoped<IAuthService, AuthService>();            
            ///////////////'
            
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            services.AddRazorPages();
            services.AddAntiforgery(o => o.HeaderName = "CSRF-TOKEN");
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Error");
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API V1");
            });

            app.UseHttpsRedirection();

            app.UseCookiePolicy();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }

}
