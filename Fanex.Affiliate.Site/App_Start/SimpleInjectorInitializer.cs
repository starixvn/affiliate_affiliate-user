// --------------------------------------------------------------------------
// <copyright file="SimpleInjectorInitializer.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.28">Define Simple Injector</change>
// </history>

namespace Fanex.Affiliate.Site.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
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
            container.RegisterMvcAttributeFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        /// <summary>
        /// Initialize Container.
        /// </summary>
        /// <param name="container">Container parameter.</param>
        private static void InitializeContainer(Container container)
        {
            container.Register<IAccountService, AccountService>();
            container.Register<IPaymentService, PaymentService>();
            container.Register<IMarketingService, MarketingService>();
            container.Register<IUserService, UserService>();
            container.Register<ISystemSettingService, SystemSettingService>();
            container.Register<IProductService, ProductService>();
            container.Register<IMessageService, MessageService>();
            container.Register<IHotNewsService, HotNewsService>();
            container.Register<IBetListService, BetListService>();
        }
    }
}