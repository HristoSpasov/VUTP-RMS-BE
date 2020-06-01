namespace RMS.IOC
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using AutoMapper;
    using Data;
    using Data.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using RMS.Data.Entities;
    using Services.Mapper;
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Static class used to initialize all dependencies
    /// </summary>
    public static class AutoFacConfig
    {
        /// <summary>
        /// API Assembly name
        /// </summary>
        private const string ApiAssemblyName = "RMS.Api";

        /// <summary>
        /// Service Assembly name
        /// </summary>
        private const string ServicesAssemblyName = "RMS.Services";

        /// <summary>
        /// Service Assembly name
        /// </summary>
        private const string RepositoryAssemblyName = "RMS.Repositories";

        /// <summary>
        /// Here are registered all dependencies using AutoFac.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        /// /// <param name="hostingEnvironment">HostingEnvironment parameter.</param>
        /// <param name="services">Contract for a collection of service descriptors.</param>
        /// <returns>Container with all registered dependencies</returns>
        public static IContainer Initialize(string connectionString, IHostingEnvironment hostingEnvironment, IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            // Context registration
            builder.Register(x =>
            {
                DbContextOptionsBuilder optionsBuilder = null;

                if (hostingEnvironment.EnvironmentName == "Test")
                {
                    optionsBuilder = new DbContextOptionsBuilder<RMS_Db_Context>().UseInMemoryDatabase("RMSInMemoryDb");
                }
                else
                {
                    optionsBuilder = new DbContextOptionsBuilder<RMS_Db_Context>().UseSqlServer(connectionString);
                }

                var entityConfig = new EntityConfiguration();

                return new RMS_Db_Context(optionsBuilder.Options, entityConfig, new Logger<RMS_Db_Context>(new LoggerFactory()));
            })
            .AsSelf()
            .InstancePerLifetimeScope();

            // Register by convention
            var assembliesToScan = new[]
            {
                Assembly.Load(ServicesAssemblyName),
                Assembly.Load(RepositoryAssemblyName)
            };

            builder.RegisterAssemblyTypes(assembliesToScan)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Auto mapper register
            var rmsMapProfile = new RMSMapperProfile();
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(rmsMapProfile);
            }))
            .AsSelf()
            .SingleInstance();

            builder.Register(cfg =>
            {
                var config = cfg.Resolve<MapperConfiguration>();
                var ctx = cfg.Resolve<IComponentContext>();
                return config.CreateMapper(ctx.Resolve);
            })
                .As<IMapper>()
                .InstancePerLifetimeScope();

            var allTypes = assembliesToScan
                .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
                .Distinct() // avoid AutoMapper.DuplicateTypeMapConfigurationException
                .SelectMany(a => a.DefinedTypes)
                .ToArray();

            var openTypes = new[]
            {
                typeof(IValueResolver<,,>),
                typeof(IMemberValueResolver<,,,>),
                typeof(ITypeConverter<,>),
                typeof(IMappingAction<,>)
            };

            foreach (var type in openTypes.SelectMany(openType =>
                allTypes.Where(t => t.IsClass && !t.IsAbstract && ImplementsGenericInterface(t.AsType(), openType))))
            {
                builder.RegisterType(type.AsType()).InstancePerDependency();
            }

            // Register services which don't follow convention naming rules
            builder.RegisterType<SeedService>().As<ISeedService>().InstancePerLifetimeScope();

            builder.Populate(services);

            var container = builder.Build();

            // Equivalent to UpdateDatabaseToLatestVersion initialization strategy
            using (var scope = container.BeginLifetimeScope())
            {
                var db = scope.Resolve<RMS_Db_Context>();
                var seedService = scope.Resolve<ISeedService>();

                if (hostingEnvironment.EnvironmentName != "Test")
                {
                    db.Database.Migrate();
                    seedService.SeedAll();
                }
            }

            return container;
        }

        /// <summary>
        /// Check if type implements generic interface.
        /// </summary>
        /// <param name="type">Type parameter.</param>
        /// <param name="interfaceType">Generic type parameter</param>
        /// <returns>If type implements generic interface.</returns>
        private static bool ImplementsGenericInterface(Type type, Type interfaceType)
            => IsGenericType(type, interfaceType) || type.GetTypeInfo().ImplementedInterfaces.Any(@interface => IsGenericType(@interface, interfaceType));

        /// <summary>
        /// Check if type is generic.
        /// </summary>
        /// <param name="type">Type parameter.</param>
        /// <param name="genericType">Generic type parameter.</param>
        /// <returns>If type is generic.</returns>
        private static bool IsGenericType(Type type, Type genericType)
            => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
    }
}