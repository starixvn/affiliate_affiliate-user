// ----------------------------------------------------------------------
// <copyright file="SimpleInjectorInitializer.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/02/25">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.App_Start
{
    using System.Reflection;
    using DataAccess;
    using Services;
    using SimpleInjector;
    using SimpleInjector.Integration.Wcf;

    /// <summary>
    /// Simple Injector Initializer.
    /// </summary>
    public class SimpleInjectorInitializer
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

            // Register the container to the SimpleInjectorServiceHostFactory.
            SimpleInjectorServiceHostFactory.SetContainer(container);
        }

        /// <summary>
        /// Initialize Container.
        /// </summary>
        /// <param name="container">Container parameter.</param>
        private static void InitializeContainer(Container container)
        {
            container.Register<IAffiliateService, AffiliateService>();
            container.Register<IAffiliateDataAccess, AffiliateDataAccess>();
        }
    }
}