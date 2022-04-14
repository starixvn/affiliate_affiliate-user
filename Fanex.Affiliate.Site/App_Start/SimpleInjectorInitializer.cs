namespace Fanex.Affiliate.Site
{
    using System.Reflection;
    using System.Web.Mvc;
    using Fanex.Caching;
    using Fanex.Caching.Memory;
    using ONEbook.UM.Configurations;
    using ONEbook.UM.Helpers;
    using ONEbook.UM.NETFramework;
    using ONEbook.UM.Services;
    using ONEbook.UM.Transport;
    using Services;
    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;

    /// <summary>
    /// Simple Injector Initializer.
    /// </summary>
    public static class SimpleInjectorInitializer
    {
        /// <summary>
        /// Initialize function.
        /// </summary>
        public static void Initialize()
        {
            // Create the container as usual.
            var container = new Container();

            // Register my types, for instance:
            InitializeContainer(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        /// <summary>
        /// Initialize Container.
        /// </summary>
        /// <param name="container">Container parameter.</param>
        private static void InitializeContainer(Container container)
        {
            container.RegisterSingleton(new UMClientConfiguration().Initialize());
            container.RegisterSingleton<ICache>(() => new MemoryCache(new MemoryCacheOptions()));

            container.RegisterSingleton<ICacheService, CacheService>();
            container.RegisterSingleton<IUMCacheService, UMCacheService>();
            container.RegisterSingleton<IUMRestClient, UMRestClient>();
            container.RegisterSingleton<IPHelper, IPHelperImpl>();
            container.RegisterSingleton<IHttpContextHelper, HttpContextHelper>();
            container.RegisterSingleton<IMaintenanceService, MaintenanceService>();

            container.Register<IAccountService, AccountService>();
            container.Register<IPaymentService, PaymentService>();
            container.Register<IMarketingService, MarketingService>();
            container.Register<IUserService, UserService>();
            container.Register<ISystemSettingService, SystemSettingService>();
            container.Register<IProductService, ProductService>();
            container.Register<IMessageService, MessageService>();
            container.Register<IHotNewsService, HotNewsService>();
        }
    }
}