namespace Footballize.Web.Infrastructure
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;
    using Common;
    using Data;
    using Data.Repositories;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Models;
    using Services.Mapping;
    using Services.Messaging;
    using ViewModels;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMvcWithSignalR(this IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                .AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled = true)
                .AddRazorRuntimeCompilation();

            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddSignalR()
                .AddNewtonsoftJsonProtocol();

            return services;
        }

        public static IServiceCollection AddConventionalServices(this IServiceCollection services)
        {
            var serviceInterfaceType = typeof(IService);
            var singletonInterfaceType = typeof(ISingletonService);
            var scopedInterfaceType = typeof(IScopedService);

            var types = serviceInterfaceType
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);


            foreach (var type in types)
            {
                if (serviceInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
                else if (singletonInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddSingleton(type.Service, type.Implementation);
                }
                else if (scopedInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddScoped(type.Service, type.Implementation);
                }
            }

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(IdentityOptionsProvider.GetIdentityOptions)
                .AddEntityFrameworkStores<FootballizeDbContext>()
                .AddUserStore<FootballizeUserStore>()
                .AddRoleStore<FootballizeRoleStore>()
                .AddDefaultTokenProviders();

            services.AddTransient<IUserStore<User>, FootballizeUserStore>();
            services.AddTransient<IRoleStore<Role>, FootballizeRoleStore>();

            return services;
        }

        public static IServiceCollection AddDefaultConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FootballizeDbContext>(options
                => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddSendGrid(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailSender>(
                serviceProvider => new SendGridEmailSender(
                    serviceProvider.GetRequiredService<ILoggerFactory>(),
                    configuration["SendGrid:ApiKey"],
                    configuration["SendGrid:SenderEmail"],
                    GlobalConstants.SystemName));

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            services.AddSingleton<IMapper>(AutoMapperConfig.MapperInstance);

            return services;
        }


        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            return services;
        }

        public static IServiceCollection AddCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Identity/Account/LogIn");

            return services;
        }
    }
}