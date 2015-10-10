using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using MMC.Client.Contracts;
using MMC.Client.Proxies.Proxies;
using Core.Common.Contracts;
using MMC.Client.Proxies;
using MMC.Web.Model;
using MMC.Web.Contracts;
using MMC.Web.Services;
namespace MMC.Client.Bootstrapper
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
      // register all your components with the container here
      // it is NOT necessary to register your controllers
      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);
      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        container.RegisterType<ILocationService, LocationClient>();
        container.RegisterType<IServiceFactory, ServiceFactory>();
        container.RegisterType<IHomeDataService, HomeDataService>();
        container.RegisterTypes(AllClasses.FromLoadedAssemblies(),
               WithMappings.FromMatchingInterface,
               WithName.Default);        
    }
  }
}