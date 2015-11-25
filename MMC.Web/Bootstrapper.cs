using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using MMC.Web.Core;
using MMC.Web.Adapters;
using MMC.Web.Controllers;
using MMC.Web.Contracts;
using MMC.Web.Services;

namespace MMC.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();           
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypes(AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default);

            container.RegisterType<ISearchDataService, SearchDataService>();
            container.RegisterType<IHomeDataService,HomeDataService>();
            container.RegisterType<ILocationDataService, LocationDataService>();
        }
    }
}