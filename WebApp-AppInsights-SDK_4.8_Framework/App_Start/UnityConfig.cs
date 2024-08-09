using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WebApp_AppInsights_SDK_4._8_Framework.Logging;

namespace WebApp_AppInsights_SDK_4._8_Framework
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ILogger, Logger>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}