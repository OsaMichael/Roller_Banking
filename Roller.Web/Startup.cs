using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Roller.DataContext.Entity;
using Roller.DContext;
using Roller.Repository;
using Roller.Repository.AccountRepo;
using Roller.Repository.CashierRepo;
using Roller.Repository.Customers;
using Roller.Repository.EmailSentLogs;
using Roller.Repository.LoanRepos;
using Roller.Repository.Services;
using Roller.Repository.UserClaim;
using Roller.Web.AutoMapperProfile;
using Roller.Web.Models;
using Roller.Web.Utility;
using Swashbuckle.AspNetCore.Swagger;

namespace Roller.Web
{
    public class Startup
    {

        public Autofac.IContainer ApplicationContainer { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //using inbuilt sql
          //  services.AddDbContext<RollerDataContext>(options => options.UseSqlServer(Configuration["DefaultConnection"]));

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
         //   services.AddDbContext<RollerDataContext>(options =>
         //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // HangFire starts here
            services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
            //  // HangFire ends here

            services.AddDbContext<RollerDataContext>(options =>
            {

                // Configure the context to use Microsoft SQL Server.
                options.UseSqlServer(connectionString, m =>
                {

                    m.MigrationsAssembly(typeof(Startup).Assembly.FullName);
                    m.EnableRetryOnFailure(5);
                });

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                //options.UseOpenIddict<long>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ROLLER BANKING API", Version = "v1" });
                // c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "password",
                    TokenUrl = "/connect/token",
                    Description = "Note: Leave client_id and client_secret blank"
                });
            });
            // services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddIdentity<User, Role>()
                 //services.AddDefaultIdentity<User>()
              
               .AddRoles<Role>()
               .AddUserManager<UserManager<User>>()
               .AddRoleManager<RoleManager<Role>>()
               .AddSignInManager<SignInManager<User>>()
               .AddEntityFrameworkStores<RollerDataContext>();

            //   services.AddTransient<INotificationRepository, NotificationRepository>();
             services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<ICashierRepository, CashierRepository>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IEmailSentLog, EmailSentLogRepository>();
            services.AddSingleton<IPaymentService, PaymentService>();
            services.AddSingleton<IClaimRepo, ClaimRepo>();
            
            //.AddDefaultTokenProviders();
            services.AddScoped<INotification, Notification>();
            services.AddScoped<IMainNotification, MainNotification>();
            services.AddScoped<IEmailService, Repository.ElasticEmailService>();
           

            //  services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policyBuilder =>
                    policyBuilder.RequireClaim(Claims.Admin, "true"));

                options.AddPolicy("User", policyBuilder =>
                    policyBuilder.RequireClaim(Claims.User, "true"));

                options.AddPolicy("Cashier", policyBuilder =>
                  policyBuilder.RequireClaim(Claims.Cashier, Claims.Admin,  "true"));
            });


            // Adding Authentication using Core Identity default authentication as well as JwtBearers scheme for the API-controller
            services.AddAuthentication()
               .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
               {
                   x.RequireHttpsMetadata = true;
                   x.SaveToken = true;
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["AppSettings:SecretKey"])),
                       ValidateIssuer = true,
                       ValidIssuer = Configuration["Issuer"],
                       ValidateAudience = true,
                       ValidAudience = Configuration["Audiance"]
                   };
               });

            // Setting redirect paths
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/Denied";
            });


            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CustomerProfile>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var mailSetting = Configuration.GetSection("ElasticEmail");
            // var mailSetting = Configuration.GetSection("EmailSetting");
            services.Configure<EmailSettings>(mailSetting);


            var payTrans = Configuration.GetSection("PayService");
            services.Configure<Pay>(payTrans);
            
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(5);
            //});

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterModule<AutofacRepoModule>();

            ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime appLifetime, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }

            app.UseSwagger();
            //app.UseMvc();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Swagger UI - ROLLER BANKING Application";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ROLLER BANKING Application API V1");
            });
            /////////////////////////////////////////////////////////////////////////
            ///
            var appSettingsSection = Configuration.GetSection("AppSettings:BackgroundJobDisplay").Value;
            if (appSettingsSection == "TRUE")
            {
                app.UseHangfireDashboard("/BackGroundService", new DashboardOptions
                {
                    Authorization = new[] { new HangFireAuthorizationFilter() }
                });
            }
            app.UseHangfireServer();
            app.UseMiddleware(typeof(HangFireNotifcation));
            //app.UseMiddleware(typeof(HangFireNotifcation));
            ///////////////////////////////////////////////////////////
            // RecurringJob.AddOrUpdate<INotification>(x => x.SendMaill(), "*/2 * * * *");

            // RecurringJob.AddOrUpdate<IMainNotification>(x => x.SendMaill2(), Cron.Minutely);
            // var id = BackgroundJob.Enqueue<INotification>(x => x.SendMaill());

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Home}/{id?}");
            });
           CreateUserRoles(services).Wait();
           appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var user = new User() { Email = Constants.Email, UserName = Constants.UserName };
            //var user = new User() { Email = "admin@gmail.com", UserName = "admin" };

            var result = await UserManager.CreateAsync(user, Constants.AdminPassword);
            await UserManager.AddClaimAsync(user, new Claim("sub", user.Id.ToString()));

            //var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            //if (!roleCheck)
            //{
            //    //create the roles and seed them to the database  
            //    await RoleManager.CreateAsync(new Role("Admin"));
            //}
            //await UserManager.AddToRoleAsync(user, "Admin");

        }
    }
}
