namespace Footballize.Web
{
    using System.Globalization;
    using System.Reflection;
    using AutoMapper;
    using Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Data;
    using Data.Repositories;
    using Data.Seeding;
    using Footballize.Models;
    using Hubs;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Middlewares;
    using ViewModels;
    using Services.Mapping;
    using Services.Messaging;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.NumberGroupSeparator = ",";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            services.AddDbContext<FootballizeDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>(IdentityOptionsProvider.GetIdentityOptions)
                .AddEntityFrameworkStores<FootballizeDbContext>()
                .AddUserStore<FootballizeUserStore>()
                .AddRoleStore<FootballizeRoleStore>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddControllersWithViews(options => 
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                .AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled = true)
                .AddRazorRuntimeCompilation();

            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddSignalR()
                .AddNewtonsoftJsonProtocol();

            services.AddApplicationInsightsTelemetry();

            
            // Identity stores
            services.AddTransient<IUserStore<User>, FootballizeUserStore>();
            services.AddTransient<IRoleStore<Role>, FootballizeRoleStore>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Application services
            services.AddTransient<IEmailSender>(
                serviceProvider => new SendGridEmailSender(
                    serviceProvider.GetRequiredService<ILoggerFactory>(),
                    this.Configuration["SendGrid:ApiKey"],
                    this.Configuration["SendGrid:SenderEmail"],
                    GlobalConstants.SystemName));

            services.AddConventionalServices();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            services.AddSingleton<IMapper>(AutoMapperConfig.MapperInstance);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<FootballizeDbContext>();
                dbContext.Database.Migrate();
                ApplicationDbContextSeeder.Seed(dbContext, serviceScope.ServiceProvider);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Home/Error?code={0}");
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<RemoveBannedUserMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute("identity","identity","Identity/{controller=Home}/{action=Index}"
                 );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                 );

                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapRazorPages();
            });
        }
    }
}
