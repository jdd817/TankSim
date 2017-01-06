[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Tank.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Tank.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Tank.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Web.Http;
    using BattleNetApi;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                // Set Web API Resolver
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load<Tank.CompositionRoot>();

            kernel.Bind<IBattleNetConfiguration>().To<BattleNetConfig>();

            kernel.Load<BattleNetModule>();
        }
    }

    public class BattleNetConfig : IBattleNetConfiguration
    {
        public string ApiKey { get { return "nryb77x56ta2dmvpykhdwdc8kf56mym8"; } set { } }

        public string ApiUrl { get { return "https://us.api.battle.net/wow"; } set { } }

        public string Locale { get { return "en_us"; } set { } }
    }
}
