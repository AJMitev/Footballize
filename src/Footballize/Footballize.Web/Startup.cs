namespace Footballize.Web
{
    using System.Reflection;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Data;
    using Data.Repositories;
    using Data.Seeding;
    using Footballize.Models;
    using ViewModels;
    using Services;
    using Services.Data;
    using Services.Mapping;

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

            services.AddDbContext<FootballizeDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>(IdentityOptionsProvider.GetIdentityOptions)
                .AddEntityFrameworkStores<FootballizeDbContext>()
                .AddUserStore<FootballizeUserStore>()
                .AddRoleStore<FootballizeRoleStore>()
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4);

            // Seed data on application startup
            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<FootballizeDbContext>();
                dbContext.Database.Migrate();
                ApplicationDbContextSeeder.Seed(dbContext, serviceScope.ServiceProvider);
            }

            // Auto Mapper Configurations
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new CountryMappingProfile());
            //});

            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);


            services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                .AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled = true)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Identity stores
            services.AddTransient<IUserStore<User>, FootballizeUserStore>();
            services.AddTransient<IRoleStore<Role>, FootballizeRoleStore>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Application services
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IProvinceServices, ProvinceService>();
            services.AddTransient<ITownService, TownService>();
            services.AddTransient<IPitchService, PitchService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IGatherServices, GatherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
