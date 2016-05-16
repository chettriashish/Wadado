using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Core.Common.Contracts;
using MMC.Client.Proxies;
using MMC.Client.Contracts;
using MMC.Login.Contracts;
using MMC.Login;
using MMC.Web.Core;
using MMC.Web.Adapters;
using System.Web.Http;
using WebApiContrib.IoC.Unity;

namespace MMC.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            //To support Web Api
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypes(AllClasses.FromLoadedAssemblies(),
                        WithMappings.FromMatchingInterface,
                        WithName.Default);
            container.RegisterType<IServiceFactory, ServiceFactory>();
            container.RegisterType<IActivitiesService, ActivityClient>();
            container.RegisterType<ILocationService, LocationClient>();
            container.RegisterType<IUsersService, UserClient>();
            container.RegisterType<ILoginService, LoginService>();
            container.RegisterType<ISecurityAdapter, SecurityAdapter>();
        }
    }
}